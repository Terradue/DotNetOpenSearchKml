//
//  KmlOpenSearchResult.cs
//
//  Author:
//       Emmanuel Mathot <emmanuel.mathot@terradue.com>
//
//  Copyright (c) 2014 Terradue
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using System;
using Terradue.OpenSearch.Result;
using Terradue.ServiceModel.Syndication;

namespace Terradue.OpenSearch.Kml {
    public class KmlOpenSearchResult : IOpenSearchResultCollection {
        public KmlOpenSearchResult() {
        }

        #region IOpenSearchResultCollection implementation

        public string Id {
            get {
                throw new NotImplementedException();
            }
        }

        public void SerializeToStream(System.IO.Stream stream) {
            throw new NotImplementedException();
        }

        public string SerializeToString() {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<IOpenSearchResultItem> Items {
            get {
                throw new NotImplementedException();
            }
        }

        public System.Collections.ObjectModel.Collection<SyndicationLink> Links {
            get {
                throw new NotImplementedException();
            }
        }

        public SyndicationElementExtensionCollection ElementExtensions {
            get {
                throw new NotImplementedException();
            }
        }

        public string Title {
            get {
                throw new NotImplementedException();
            }
        }

        public DateTime Date {
            get {
                throw new NotImplementedException();
            }
        }

        public string Identifier {
            get {
                throw new NotImplementedException();
            }
        }

        public long Count {
            get {
                throw new NotImplementedException();
            }
        }

        public string ContentType {
            get {
                throw new NotImplementedException();
            }
        }

        public bool ShowNamespaces {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        #endregion

        public static KmlOpenSearchResult CreateFromOpenSearchResultCollection(AtomFeed feed) {
            throw new NotImplementedException();
        }
    }
}

