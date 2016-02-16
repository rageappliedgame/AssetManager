// <copyright file="iasset.cs" company="RAGE">
// Copyright (c) 2015 RAGE. All rights reserved.
// </copyright>
// <author>Veg</author>
// <date>10-4-2015</date>
// <summary>Implements the iasset class</summary>
namespace AssetPackage
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for asset.
    /// </summary>
    public interface IAsset
    {
        #region Properties

        /// <summary>
        /// Gets the class.
        /// </summary>
        ///
        /// <value>
        /// The class.
        /// </value>
        String Class
        {
            get;
        }

        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        ///
        /// <value>
        /// The dependencies (A Dictionary of class=version pairs).
        /// </value>
        Dictionary<String, String> Dependencies
        {
            get;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        String Id
        {
            get;
        }

        /// <summary>
        /// Gets the maturity.
        /// </summary>
        ///
        /// <value>
        /// The maturity.
        /// </value>
        String Maturity
        {
            get;
        }

        /// <summary>
        /// Gets or sets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The settings.
        /// </value>
        ISettings Settings
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
        String Version
        {
            get;
        }

        /// <summary>
        /// Gets or sets the bridge.
        /// </summary>
        ///
        /// <value>
        /// The bridge.
        /// </value>
        IBridge Bridge
        {
            get;
            set;
        }

        #endregion Properties
    }
}