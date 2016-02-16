// <copyright file="IDataStorage.cs" company="RAGE"> Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>13-4-2015</date>
// <summary>Defines the IDataStorage Interface</summary>
namespace AssetPackage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for data storage.
    /// </summary>
    public interface IDataStorage
    {
        #region Methods

        /// <summary>
        /// Deletes the given fileId.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        Boolean Delete(String fileId);

        /// <summary>
        /// Check if exists the file with the given identifier.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to delete. </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        Boolean Exists(String fileId);

        /// <summary>
        /// Gets the files.
        /// </summary>
        ///
        /// <remarks>
        /// A List&lt;String&gt; gave problems when compiled as PCL and added to a
        /// Xamarin Forms project containing iOS, Android and WinPhone subprojects.
        /// </remarks>
        ///
        /// <returns>
        /// An array of filenames.
        /// </returns>
        String[] Files();

        /// <summary>
        /// Loads the given file.
        /// </summary>
        ///
        /// <param name="fileId"> The file identifier to load. </param>
        ///
        /// <returns>
        /// A String with with the file contents.
        /// </returns>
        String Load(String fileId);

        /// <summary>
        /// Saves the given file.
        /// </summary>
        ///
        /// <param name="fileId">   The file identifier to delete. </param>
        /// <param name="fileData"> Information describing the file. </param>
        void Save(String fileId, String fileData);

        #endregion Methods
    }
}