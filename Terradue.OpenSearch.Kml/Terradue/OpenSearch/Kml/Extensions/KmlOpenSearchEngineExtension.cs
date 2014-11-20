using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using Terradue.OpenSearch;
using Terradue.OpenSearch.Engine.Extensions;
using Terradue.OpenSearch.Engine;
using Terradue.OpenSearch.Response;
using Terradue.OpenSearch.Result;
using Terradue.ServiceModel.Syndication;
using Terradue.OpenSearch.Kml;
using SharpKml.Dom;
using SharpKml.Base;
using Terradue.OpenSearch.Kml.Result;
using Terradue.OpenSearch.Request;
using Mono.Addins;

[assembly:Addin]
[assembly:AddinDependency ("OpenSearchEngine", "1.0")]

namespace Terradue.OpenSearch.Kml.Extensions {
    public enum COORDINATE {
        LATLONG,
        LONGLAT}
    ;

    public enum EGEOMETRYTYPE {
        NONE,
        Curve,
        Grid,
        LinearRing,
        LineString,
        MultiCurve,
        MultiGeometry,
        MultiPoint,
        MultiSolid,
        MultiSurface,
        Point,
        Polygon,
        RectifiedGrid,
        Surface}
    ;

    public enum EDIMENSION {
        TWO,
        THREE}
    ;


    [Extension(typeof(IOpenSearchEngineExtension))]
    [ExtensionNode("GeoJson", "GeoJson native query")]
    public class KMLAssemblyOpenSearchEngineExtension : OpenSearchEngineExtension<KmlOpenSearchResult> {

        public const string KmlMimeType = "application/vnd.google-earth.kml+xml";
                                          
        // default constructor method
        public KMLAssemblyOpenSearchEngineExtension() {
        }

        #region implemented abstract members of OpenSearchEngineExtension


        public override string Name {
            get {
                return "Kml for EO";
            }
        }

        public override string Identifier {
            get {
                return "kml";
            }
        }

        public override IOpenSearchResultCollection ReadNative(OpenSearchResponse response) {
            throw new NotSupportedException("Kml extension does not transform OpenSearch response from " + response.ContentType);
        }

        public void ApplyResultFilters(OpenSearchEngine ose, OpenSearchRequest request, IOpenSearchResultCollection osr) {

        }

        public override OpenSearchUrl FindOpenSearchDescriptionUrlFromResponse(OpenSearchResponse response) {
            throw new NotImplementedException();
        }

        public override string DiscoveryContentType {
            get {
                return KmlMimeType;
            }
        }

        #endregion

        public override IOpenSearchResultCollection CreateOpenSearchResultFromOpenSearchResult(IOpenSearchResultCollection results) {
            if (results is KmlOpenSearchResult) return results;

            KmlOpenSearchResult kml = KmlOpenSearchResult.CreateFromOpenSearchResultCollection(results);

            return kml;


			
        }

    }
}



