namespace AssetPackage
{
    using System;

    /// <summary>
    /// Interface for default settings.<br /><br />This Interface is used to:
    /// <list type="bullet">
    /// <item>Check if an asset has default
    /// (application) settings that override build-in default settings.</item>
    /// <item>Load these settings from the game environment.</item>
    /// <item>In certain environments write the actual settings as application defaults. This could
    /// for instance be Unity in editor mode.</item>
    /// </list>
    /// </summary>
    ///
    /// <remarks>
    /// <para>Default settings and application default settings are read-only at run-
    /// time.</para><para>If modification and storage is needed at run-time,
    /// the<see cref="IDataStorage"/>interface could be used
    /// i.c.m.<see cref="ISettings"/>Methods.</para>
    /// <para>This interface, if implemented in a bridge, allows to check if an <see cref="BaseAsset"/>
    /// has some default settings</para>
    /// </remarks>
    public interface IDefaultSettings
    {
        #region Methods

        /// <summary>
        /// Query if a 'Class' with Id has default settings.
        /// </summary>
        ///
        /// <param name="Class"> The class. </param>
        /// <param name="Id">    The identifier. </param>
        ///
        /// <returns>
        /// true if default settings, false if not.
        /// </returns>
        Boolean HasDefaultSettings(String Class, String Id);

        /// <summary>
        /// Loads default settings for a 'Class' with Id.
        /// </summary>
        ///
        /// <remarks>
        /// Note that in Unity the file has to be located in the Resource Directory of the Assets Folder.
        /// </remarks>
        ///
        /// <param name="Class"> The class. </param>
        /// <param name="Id">    The identifier. </param>
        ///
        /// <returns>
        /// The default settings.
        /// </returns>
        String LoadDefaultSettings(String Class, String Id);

        /// <summary>
        /// Saves a default settings for a 'Class' with Id.
        /// </summary>
        ///
        /// <remarks>
        /// This method can only be used during editing the game (so NOT at run-time).
        /// </remarks>
        ///
        /// <param name="Class">    The class. </param>
        /// <param name="Id">       The identifier. </param>
        /// <param name="fileData"> The File Data. </param>
        void SaveDefaultSettings(String Class, String Id, String fileData);

        #endregion Methods
    }
}