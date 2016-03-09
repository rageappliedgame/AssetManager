// <copyright file="BaseAsset.cs" company="RAGE">
// Copyright (c) 2015 RAGE All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Implements the base asset class</summary>
namespace AssetPackage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

#if PORTABLE
    // System.Reflection is needed for Portable Assemblies!
    using System.Reflection;
#endif

    using AssetManagerPackage;

    /// <summary>
    /// A base asset.
    /// </summary>
    public class BaseAsset : IAsset
    {
        private ILog logger = null;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AssetManagerPackage.BaseAsset class.
        /// </summary>
        public BaseAsset()
        {
            this.Id = AssetManager.Instance.registerAssetInstance(this, this.Class);

            //! List Embedded Resources.
            //foreach (String name in Assembly.GetCallingAssembly().GetManifestResourceNames())
            //{
            //    Console.WriteLine("{0}", name);
            //}
            String xml = VersionAndDependencies();
            if (!String.IsNullOrEmpty(xml))
            {
                this.VersionInfo = RageVersionInfo.LoadVersionInfo(xml);
            }
            else
            {
                Log(Severity.Verbose, "{0} VersionInfo is missing", GetType().Name);
            }
        }

        /// <summary>
        /// Initializes a new instance of the AssetPackage.BaseAsset class.
        /// </summary>
        ///
        /// <param name="bridge"> The bridge. </param>
        public BaseAsset(IBridge bridge)
            : this()
        {
            this.Bridge = bridge;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the bridge.
        /// </summary>
        ///
        /// <value>
        /// The bridge.
        /// </value>
        public IBridge Bridge
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the class.
        /// </summary>
        ///
        /// <value>
        /// The class.
        /// </value>
        public String Class
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        ///
        /// <value>
        /// The dependencies.
        /// </value>
        public Dictionary<String, String> Dependencies
        {
            get
            {
                Dictionary<String, String> result = new Dictionary<String, String>();

                foreach (Depends dep in VersionInfo.Dependencies)
                {
                    String minv = dep.minVersion != null ? dep.minVersion : "0.0";
                    String maxv = dep.maxVersion != null ? dep.maxVersion : "*";

                    result.Add(dep.name, String.Format("{0}-{1}", minv, maxv));
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this object has settings.
        /// </summary>
        ///
        /// <value>
        /// true if this object has settings, false if not.
        /// </value>
        public Boolean hasSettings
        {
            get
            {
                return Settings != null;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        public String Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the maturity.
        /// </summary>
        ///
        /// <value>
        /// The maturity.
        /// </value>
        public String Maturity
        {
            get
            {
                return VersionInfo.Maturity;
            }
        }

        /// <summary>
        /// Gets or sets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The settings.
        /// </value>
        public virtual ISettings Settings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        ///
        /// <value>
        /// The version.
        /// </value>
        public String Version
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
                        VersionInfo.Major,
                        VersionInfo.Minor,
                        VersionInfo.Build,
                        VersionInfo.Revision == 0 ? "" : VersionInfo.Revision.ToString()
                    ).TrimEnd('.');
            }
        }

        /// <summary>
        /// Gets information describing the version.
        /// </summary>
        ///
        /// <value>
        /// Information describing the version.
        /// </value>
        public RageVersionInfo VersionInfo
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Logs.
        /// </summary>
        ///
        /// <param name="severity"> The severity. </param>
        /// <param name="format">   Describes the format to use. </param>
        /// <param name="args">     A variable-length parameters list containing
        ///                         arguments. </param>
        public void Log(Severity severity, String format, params object[] args)
        {
            Log(severity, String.Format(format, args));
        }

        /// <summary>
        /// Logs.
        /// </summary>
        ///
        /// <param name="severity"> The severity. </param>
        /// <param name="msg">      The message. </param>
        public void Log(Severity severity, String msg)
        {
            logger = getInterface<ILog>();

            if (logger != null)
            {
                logger.Log(severity, msg);
            }
        }

        /// <summary>
        /// Loads Settings object from Default (Design-time) Settings.
        /// </summary>
        ///
        /// <remarks>
        /// In Unity Resources.Load() must be used and the files will be loaded a Assets\\Resources
        /// Folder.
        /// </remarks>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public Boolean LoadDefaultSettings()
        {
            IDefaultSettings ds = getInterface<IDefaultSettings>();

            if (ds != null && hasSettings && ds.HasDefaultSettings(Class, Id))
            {
                String xml = ds.LoadDefaultSettings(Class, Id);

                Settings = SettingsFromXml(xml);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads Settings object as Run-time Settings.
        /// </summary>
        ///
        /// <remarks>
        /// The resulting file will be read using the IDataStorage interface.
        /// </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public Boolean LoadSettings(String filename)
        {
            IDataStorage ds = getInterface<IDataStorage>();

            if (ds != null && hasSettings && ds.Exists(filename))
            {
                String xml = ds.Load(filename);

                Settings = SettingsFromXml(xml);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Saves Settings object as Default (Design-time) Settings.
        /// </summary>
        ///
        /// <remarks>
        /// In Unity the file will be saved in a Assets\\Resources Folder in the editor environment (As
        /// resources are read-only at run-time).
        /// </remarks>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public Boolean SaveDefaultSettings(bool force)
        {
            IDefaultSettings ds = getInterface<IDefaultSettings>();

            if (ds != null && hasSettings && (force || !ds.HasDefaultSettings(Class, Id)))
            {
                ds.SaveDefaultSettings(Class, Id, SettingsToXml());

                return true;
            }

            return false;
        }

        /// <summary>
        /// Save Settings object from Run-time Settings.
        /// </summary>
        ///
        /// <remarks>
        /// The resulting file will be written using the IDataStorage interface.
        /// </remarks>
        ///
        /// <param name="filename"> Filename of the file. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public Boolean SaveSettings(String filename)
        {
            IDataStorage ds = getInterface<IDataStorage>();

            if (ds != null && hasSettings)
            {
                ds.Save(filename, SettingsToXml());

                return true;
            }

            return false;
        }

        /// <summary>
        /// Settings from XML.
        /// </summary>
        ///
        /// <param name="xml"> The XML. </param>
        ///
        /// <returns>
        /// The ISettings.
        /// </returns>
        public ISettings SettingsFromXml(String xml)
        {
            XmlSerializer ser = new XmlSerializer(Settings.GetType());

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                //! Use DataContractSerializer or DataContractJsonSerializer?
                //
                return (ISettings)ser.Deserialize(ms);
            }
        }

        /// <summary>
        /// Settings to XML.
        /// </summary>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        public String SettingsToXml()
        {
            XmlSerializer ser = new XmlSerializer(Settings.GetType());

            using (StringWriterUtf8 textWriter = new StringWriterUtf8())
            {
                //! Use DataContractSerializer or DataContractJsonSerializer?
                // See https://msdn.microsoft.com/en-us/library/bb412170(v=vs.100).aspx
                // See https://msdn.microsoft.com/en-us/library/bb924435(v=vs.110).aspx
                // See https://msdn.microsoft.com/en-us/library/aa347875(v=vs.110).aspx
                //
                ser.Serialize(textWriter, Settings);

                textWriter.Flush();

                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Version and dependencies.
        /// </summary>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        internal String VersionAndDependencies()
        {
            // Not compatible with PCL
            // 
            //foreach (String res in GetType().Assembly.GetManifestResourceNames())
            //{
            //    Debug.WriteLine(res);
            //}

            //! asset_proof_of_concept_demo_CSharp.Resources.Asset.VersionAndDependencies.xml
            //! <namespace>.Resources.<AssetType>.VersionAndDependencies.xml
            //
            String xml = GetEmbeddedResource(GetType().Namespace, String.Format("Resources.{0}.VersionAndDependencies.xml", GetType().Name));

            //{
            // Load- embedded resource in-xamarin.
            // 
            // IEmbeddedResource er = getInterface<IEmbeddedResource>();
            // String path = er.RetrieveResource(String.Format("Resources.{0}.VersionAndDependencies.xml", GetType().Name));

            // xml = er.RetrieveResource(path);
            //}

            return String.IsNullOrEmpty(xml) ? String.Empty : xml;
        }

        /// <summary>
        /// Gets embedded resource.
        /// </summary>
        ///
        /// <param name="ns">  The namespace. </param>
        /// <param name="res"> The resource name. </param>
        ///
        /// <returns>
        /// The embedded resource.
        /// </returns>
        protected String GetEmbeddedResource(String ns, String res)
        {
            String path = String.Format("{0}.{1}", ns, res);

            //! 0) AppDomain is not present in Unity/WP81
            //Console.WriteLine(AppDomain.CurrentDomain.GetAssemblies().
            //       SingleOrDefault(assembly => assembly.GetName().Name == "HATAsset"));

            //! 0) Returns RageAssetManager.dll instead of the asset
            //using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            //Assembly asm = AssetAssembly;

            //if (asm != null)
            //{
            // using (Stream stream = asm.GetManifestResourceStream(path))

            //! 1) Compiles but fails on Unity/WP81 with a console dump
            //using (Stream stream = Assembly.GetAssembly(GetType()).GetManifestResourceStream(path))

            // Console.WriteLine("Loading Resources: {0}",path);
#if PORTABLE
            //! 2) Fails to compile on non portable projects as GetTypeInfo is missing. 
            //!    GetType.Assembly does not exits in portable projects.
            using (Stream stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream(path))
#else
            //! 3) Fail to compile on Unity3D/WinPhone (getAssembly fails) but the code works!
            using (Stream stream = GetType().Assembly.GetManifestResourceStream(path))
#endif
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            //}

            return String.Empty;
        }

        /// <summary>
        /// Gets the interface.
        /// </summary>
        ///
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        ///
        /// <returns>
        /// The interface.
        /// </returns>
        protected T getInterface<T>()
        {
            if (Bridge != null && Bridge is T)
            {
                return (T)Bridge;
            }
            else if (AssetManager.Instance.Bridge != null && AssetManager.Instance.Bridge is T)
            {
                return (T)(AssetManager.Instance.Bridge);
            }

            return default(T);
        }

        #endregion Methods

        #region Nested Types

        /// <summary>
        /// A string writer utf-8.
        /// </summary>
        ///
        /// <remarks>
        /// Fix-up for XDocument Serialization defaulting to utf-16.
        /// </remarks>
        internal class StringWriterUtf8 : StringWriter
        {
            #region Properties

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}