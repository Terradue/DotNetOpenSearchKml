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
using SharpKml.Dom;
using SharpKml.Base;
using System.Xml;
using System.Text;
using Terradue.OpenSearch.Kml.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Terradue.GeoJson.Geometry;
using Terradue.GeoJson;

namespace Terradue.OpenSearch.Kml.Result {
    public class KmlOpenSearchResultCollection : IOpenSearchResultCollection {

        public const string GEORSS = "http://www.georss.org/georss";
        public const string GEORSS10 = "http://www.georss.org/georss/10";

        SharpKml.Dom.Kml kml;

        public KmlOpenSearchResultCollection() {
            kml = new SharpKml.Dom.Kml();
        }

        public SharpKml.Dom.Kml Kml {
            get {
                return kml;
            }
        }

        #region IOpenSearchResultCollection implementation

        public string Id {
            get {
                throw new NotImplementedException();
            }
            set{ }
        }

        public void SerializeToStream(System.IO.Stream stream) {
            Serializer serializer = new Serializer();
            serializer.Serialize(kml);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] result = encoding.GetBytes(serializer.Xml);
            stream.Write(result, 0, result.Length);
        }

        public string SerializeToString() {
            Serializer serializer = new Serializer();
            serializer.Serialize(kml);
            return serializer.Xml;
        }

        public System.Collections.Generic.IEnumerable<IOpenSearchResultItem> Items { get; set; }

        public System.Collections.ObjectModel.Collection<SyndicationLink> Links { get; set; }

        public SyndicationElementExtensionCollection ElementExtensions { get; set; }

        public TextSyndicationContent Title { get; set; }

        public DateTime Date { get; set; }

        public string Identifier { get; set; }

        public long Count { get; protected set; }

        public string ContentType {
            get {
                return KMLAssemblyOpenSearchEngineExtension.KmlMimeType;
            }
        }

        public bool ShowNamespaces { get; set; }

        readonly System.Collections.ObjectModel.Collection<SyndicationCategory> categories;

        public System.Collections.ObjectModel.Collection<SyndicationCategory> Categories {
            get {
                return categories;
            }
        }

        readonly System.Collections.ObjectModel.Collection<SyndicationPerson> authors;

        public System.Collections.ObjectModel.Collection<SyndicationPerson> Authors {
            get {
                return authors;
            }
        }

        public long TotalResults { get; protected set; }

        readonly System.Collections.ObjectModel.Collection<SyndicationPerson> contributors;

        public Collection<SyndicationPerson> Contributors {
            get { return contributors; }
        }

        public TextSyndicationContent Copyright { get; set; }

        public TextSyndicationContent Description {
            get;
            set; 
        }

        public string Generator { get; set; }

        #endregion

        public static KmlOpenSearchResultCollection CreateFromOpenSearchResultCollection(IOpenSearchResultCollection results) {
            Document doc = new Document();

            //find who call the search, shop cart or search response
            if (results.Title != null) doc.Name = results.Title.Text;
            else doc.Name = results.Identifier;

            if (results.Description != null) {
                Description description = new Description();
                description.Text = results.Description.Text;
                doc.Description = description;
            }

            SharpKml.Dom.Data queryTime = new SharpKml.Dom.Data();
            queryTime.Name = "queryTime";
            queryTime.DisplayName = "queryTime";
            queryTime.Value = results.ElementExtensions.ReadElementExtensions<string>("queryTime", "http://a9.com/-/spec/opensearch/1.1/")[0];

            doc.ExtendedData = new ExtendedData();
            doc.ExtendedData.AddData(queryTime);

            Style headerStyle = new Style();
            headerStyle.Id = "ngEO-balloon-style";

            doc.AddStyle(headerStyle);

            LineStyle line = new LineStyle();
            line.Width = 2.0;
            line.Color = Color32.Parse("ff0000ff");

            PolygonStyle polygonStyle = new PolygonStyle();
            polygonStyle.Fill = true;
            polygonStyle.ColorMode = ColorMode.Normal;
            polygonStyle.Color = Color32.Parse("440000ff");

            foreach (IOpenSearchResultItem item in results.Items) {

                Style style = new Style();
                style.Polygon = polygonStyle;
                style.Line = line;

                Placemark placeMark = new Placemark();
                if (item.Title != null) placeMark.Name = item.Title.Text;
                else placeMark.Name = item.Identifier;
                placeMark.StyleUrl = new Uri("#ngEO-balloon-style", UriKind.Relative);

                // retrive the single earthObservation node information
                ExtendedData eData = new ExtendedData();

                // building the footprint
                XmlDocument coordinatesDoc = new XmlDocument();
                Dictionary<string, string> result = new Dictionary<string, string>();

                foreach (SyndicationElementExtension ext in item.ElementExtensions) {

                    XElement element = XElement.Load(ext.GetReader());
                    XElementToExtendedData(element, ext.OuterName, ref eData);

                    if (ext.OuterNamespace == GEORSS || ext.OuterNamespace == GEORSS10) {
                        var xml = ext.GetObject<XmlElement>();
                        var geometry = GeometryFactory.GeoRSSToGeometry(xml);
                        if (geometry != null) placeMark.Geometry = KMLGeometry(geometry);
                    }
                }

                // assigning values to the placemark
                placeMark.AddStyle(style);
                placeMark.ExtendedData = eData;

                //adding the placemark to the document
                doc.AddFeature(placeMark);
            }

            KmlOpenSearchResultCollection kmlResult = new KmlOpenSearchResultCollection();
            kmlResult.Items = results.Items;
            kmlResult.Links = results.Links;
            kmlResult.ElementExtensions = results.ElementExtensions;
            kmlResult.Title = results.Title;
            kmlResult.Date = results.Date;
            kmlResult.Identifier = results.Identifier;
            kmlResult.Count = results.Count;
            kmlResult.ShowNamespaces = results.ShowNamespaces;

            kmlResult.Kml.Feature = doc;

            return kmlResult;
        }

        public static void XElementToExtendedData(XElement element, string key, ref ExtendedData eData) {

            foreach (var subElement in element.Elements()) {
                XElementToExtendedData(subElement, key + "." + subElement.Name.LocalName, ref eData);
            }

            if (!element.HasElements) {
                Data data = new Data();
                data.Name = key + "." + element.Name.LocalName;
                data.Value = element.Value;
                eData.AddData(data);
            }

            foreach (var attr in element.Attributes()) {
                if (attr.IsNamespaceDeclaration) continue;
                if (attr.Name.LocalName == "nil") continue;
                Data data = new Data();
                data.Name = key + "." + attr.Name.LocalName;
                data.Value = attr.Value;
                eData.AddData(data);
            }

        }

        // read GML geometry type and return the correspondending kml polygon
        public static Geometry KMLGeometry(GeometryObject geometry) {
            Geometry kmlGeometry = null;

            switch (geometry.Type) {
                case GeoJsonObjectType.Polygon:
                    kmlGeometry = KmlPolygon((Terradue.GeoJson.Geometry.Polygon)geometry);
                    break;
                case GeoJsonObjectType.Point:
                    kmlGeometry = KmlPoint((Terradue.GeoJson.Geometry.Point)geometry);
                    break;
                case GeoJsonObjectType.MultiPoint:
                    kmlGeometry = KmlMultiPoint((MultiPoint)geometry);
                    break;
                case GeoJsonObjectType.MultiPolygon:
                    kmlGeometry = KmlMultiPolygon((MultiPolygon)geometry);
                    break;
                case GeoJsonObjectType.LineString:
                    kmlGeometry = KmlLineString((Terradue.GeoJson.Geometry.LineString)geometry);
                    break;
                case GeoJsonObjectType.MultiLineString:
                    kmlGeometry = KmlMultiLineString((MultiLineString)geometry);
                    break;
                default:
                    break;
            }
            return kmlGeometry;
        }

        public static SharpKml.Dom.MultipleGeometry KmlMultiPoint(MultiPoint multiPoint) {
            MultipleGeometry kmlMultiPoint = new MultipleGeometry();
            foreach (var point in multiPoint.Points) {
                SharpKml.Dom.Point kmlPoint = KmlPoint(point);
                kmlMultiPoint.AddGeometry(kmlPoint);
            }
            return kmlMultiPoint;
        }

        public static SharpKml.Dom.MultipleGeometry KmlMultiPolygon(MultiPolygon multiPolygon) {
            MultipleGeometry kmlMultiPolygon = new MultipleGeometry();
            foreach (var polygon in multiPolygon.Polygons) {
                kmlMultiPolygon.AddGeometry(KmlPolygon(polygon));
            }
            return kmlMultiPolygon;
        }

        public static SharpKml.Dom.MultipleGeometry KmlMultiLineString(MultiLineString multiLineString) {
            MultipleGeometry kmlMultiLineString = new MultipleGeometry();
            foreach (var lineString in multiLineString.LineStrings) {
                kmlMultiLineString.AddGeometry(KmlLineString(lineString));
            }
            return kmlMultiLineString;
        }

        public static SharpKml.Dom.Point KmlPoint(Terradue.GeoJson.Geometry.Point point) {
            SharpKml.Dom.Point kmlPoint = new SharpKml.Dom.Point();
            kmlPoint.Coordinate = KmlVector(point.Position);
            kmlPoint.Extrude = true;
            return kmlPoint;
        }

        public static Vector KmlVector(IPosition position) {

            if (position is GeographicPosition) {
                Vector kmlVector = new Vector();
                GeographicPosition p = (GeographicPosition)position;
                kmlVector.Altitude = p.Altitude;
                kmlVector.Latitude = p.Latitude;
                kmlVector.Longitude = p.Longitude;
                return kmlVector;
            }
            return null;
        }

        public static SharpKml.Dom.LineString KmlLineString(Terradue.GeoJson.Geometry.LineString lineString) {  

            SharpKml.Dom.LineString kmlLineString = new SharpKml.Dom.LineString();
            kmlLineString.Coordinates = KmlCoordinates(lineString.Positions.ToArray());
            kmlLineString.Extrude = true;
            return kmlLineString;
        }

        public static SharpKml.Dom.Polygon KmlPolygon(Terradue.GeoJson.Geometry.Polygon polygon) {

            SharpKml.Dom.Polygon kmlPolygon = new SharpKml.Dom.Polygon(); 
            if (polygon.LineStrings.Count > 0) {
                OuterBoundary exterior = new OuterBoundary();
                kmlPolygon.OuterBoundary = exterior;
                LinearRing kmlLinearRing = new LinearRing();
                exterior.LinearRing = kmlLinearRing;
                kmlLinearRing.Coordinates = KmlCoordinates(polygon.LineStrings[0].Positions.ToArray());
                if (polygon.LineStrings.Count > 1) {
                    foreach (var lineString in polygon.LineStrings.Take(1)) {
                        var interior = new InnerBoundary();
                        kmlLinearRing = new LinearRing();
                        interior.LinearRing = kmlLinearRing;
                        kmlLinearRing.Coordinates = KmlCoordinates(lineString.Positions.ToArray());
                        kmlPolygon.AddInnerBoundary(interior);
                    }
                }
            }

            // build the polygon structure
            kmlPolygon.Extrude = true;
            kmlPolygon.AltitudeMode = AltitudeMode.ClampToGround;  // Ignore an altitude specification
            kmlPolygon.Tessellate = true;

            return kmlPolygon;
        }

        public static CoordinateCollection KmlCoordinates(IPosition[] positions) {

            if (positions.Length > 0 && positions[0] is GeographicPosition) {
                CoordinateCollection kmlCoordinates = new CoordinateCollection();
                foreach (var position in positions) kmlCoordinates.Add(KmlVector(position));
                return kmlCoordinates;
            }
            return null;
        }

    }
}

