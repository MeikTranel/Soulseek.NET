﻿// <copyright file="BrowseResponse.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham. All rights reserved.
//
//     This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as
//     published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//     of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License along with this program. If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace Soulseek.NET.Messaging.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BrowseResponse
    {
        #region Internal Constructors

        internal BrowseResponse()
        {
        }

        #endregion Internal Constructors

        #region Public Properties

        public IEnumerable<Directory> Directories
        {
            get
            {
                return DirectoryList.AsReadOnly();
            }

            internal set
            {
                DirectoryList = value.ToList();
            }
        }

        public int DirectoryCount { get; internal set; }

        #endregion Public Properties

        #region Private Properties

        private List<Directory> DirectoryList { get; set; } = new List<Directory>();

        #endregion Private Properties

        #region Public Methods

        public static BrowseResponse Parse(Message message)
        {
            var reader = new MessageReader(message);

            if (reader.Code != MessageCode.PeerBrowseResponse)
            {
                throw new MessageException($"Message Code mismatch creating Peer Shares Response (expected: {(int)MessageCode.PeerBrowseResponse}, received: {(int)reader.Code}");
            }

            try
            {
                reader.Decompress();
            }
            catch (Exception)
            {
                // discard result if it fails to decompress
                return null;
            }

            var response = new BrowseResponse
            {
                DirectoryCount = reader.ReadInteger(),
            };

            for (int i = 0; i < response.DirectoryCount; i++)
            {
                var dir = new Directory
                {
                    Directoryname = reader.ReadString(),
                    FileCount = reader.ReadInteger(),
                };

                for (int j = 0; j < dir.FileCount; j++)
                {
                    var file = new File
                    {
                        Code = reader.ReadByte(),
                        Filename = reader.ReadString(),
                        Size = reader.ReadLong(),
                        Extension = reader.ReadString(),
                        AttributeCount = reader.ReadInteger()
                    };

                    for (int k = 0; k < file.AttributeCount; k++)
                    {
                        var attribute = new FileAttribute
                        {
                            Type = (FileAttributeType)reader.ReadInteger(),
                            Value = reader.ReadInteger()
                        };

                        file.AttributeList.Add(attribute);
                    }

                    dir.FileList.Add(file);
                }

                response.DirectoryList.Add(dir);
            }

            return response;
        }

        #endregion Public Methods
    }
}