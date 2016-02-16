using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AssetManagerPackage
{
    /// <summary>
    /// Information about the rage version.
    /// </summary>
    [XmlRoot("version")]
    public class RageVersionInfo
    {
        /// <summary>
        /// Initializes a new instance of the AssetManagerPackage.RageVersionInfo
        /// class.
        /// </summary>
        public RageVersionInfo()
        {
            Dependencies = new Dependencies();
        }

        //<version>
        //  <id>asset</id>
        //  <major>1</major>
        //  <minor>2</minor>
        //  <build>3</build>
        //  <revision></revision>
        //  <maturity>alpha</maturity>
        //  <dependencies>
        //    <depends minVersion = "1.2.3" > Logger </ depends >
        //  </ dependencies >
        //</ version >

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        [XmlElement("id")]
        public String Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the major.
        /// </summary>
        ///
        /// <value>
        /// The major.
        /// </value>
        [XmlElement("major")]
        public Int32 Major
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the minor.
        /// </summary>
        ///
        /// <value>
        /// The minor.
        /// </value>
        [XmlElement("minor")]
        public Int32 Minor
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the build.
        /// </summary>
        ///
        /// <value>
        /// The build.
        /// </value>
        [XmlElement("build")]
        public Int32 Build
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        ///
        /// <value>
        /// The revision.
        /// </value>
        [XmlElement("revision")]
        public Int32 Revision
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the maturity.
        /// </summary>
        ///
        /// <value>
        /// The maturity.
        /// </value>
        [XmlElement("maturity")]
        public String Maturity
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        ///
        /// <value>
        /// The dependencies.
        /// </value>
        [XmlArray("dependencies")]
        [XmlArrayItem("depends")]
        public Dependencies Dependencies
        {
            get; set;
        }

        /// <summary>
        /// Loads version information.
        /// </summary>
        ///
        /// <param name="xml"> The XML. </param>
        public static RageVersionInfo LoadVersionInfo(String xml)
        {

            XmlSerializer ser = new XmlSerializer(typeof(RageVersionInfo));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                //! Use DataContractSerializer or DataContractJsonSerializer?
                //
                return (RageVersionInfo)ser.Deserialize(ms);
            }
        }

        /// <summary>
        /// Saves the version information.
        /// </summary>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        public String SaveVersionInfo()
        {
            XmlSerializer ser = new XmlSerializer(GetType());

            using (StringWriterUtf8 textWriter = new StringWriterUtf8())
            {
                //! Use DataContractSerializer or DataContractJsonSerializer?
                // See https://msdn.microsoft.com/en-us/library/bb412170(v=vs.100).aspx
                // See https://msdn.microsoft.com/en-us/library/bb924435(v=vs.110).aspx
                // See https://msdn.microsoft.com/en-us/library/aa347875(v=vs.110).aspx
                //
                ser.Serialize(textWriter, this);

                textWriter.Flush();

                return textWriter.ToString();
            }
        }

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

            //public StringWriterUtf8(StringBuilder sb)
            //    : base(sb)
            //{
            //}
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }

            #endregion Properties
        }
    }

    /// <summary>
    /// A dependencies.
    /// </summary>
    [XmlRoot("dependencies")]
    public class Dependencies : List<Depends>
    {
        /// <summary>
        /// Initializes a new instance of the AssetManagerPackage.dependencies
        /// class.
        /// </summary>
        public Dependencies() : base()
        {
            //
        }

        //  <dependencies>
        //    <depends minVersion = "1.2.3" > Logger </ depends >
        //  </ dependencies >

    }

    /// <summary>
    /// A dependency.
    /// </summary>
    [XmlRoot("depends")]
    public class Depends
    {
        /// <summary>
        /// Initializes a new instance of the AssetManagerPackage.Dependency
        /// class.
        /// </summary>
        public Depends()
        {
            //
        }

        //    <depends minVersion = "1.2.3" > Logger </ depends >

        /// <summary>
        /// Gets or sets the minimum version.
        /// </summary>
        ///
        /// <value>
        /// The minimum version.
        /// </value>
        [XmlAttribute("minVersion")]
        public String minVersion
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the maximum version.
        /// </summary>
        ///
        /// <value>
        /// The maximum version.
        /// </value>
        [XmlAttribute("maxVersion")]
        public String maxVersion
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        [XmlText]
        public String name
        {
            get; set;
        }
    }
}
