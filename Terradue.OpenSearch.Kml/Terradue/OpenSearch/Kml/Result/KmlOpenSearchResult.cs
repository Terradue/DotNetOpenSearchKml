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

namespace Terradue.OpenSearch.Kml.Result {
    public class KmlOpenSearchResult : IOpenSearchResultCollection {

        SharpKml.Dom.Kml kml;

        public KmlOpenSearchResult() {
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
            throw new NotImplementedException();
        }

        public string SerializeToString() {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<IOpenSearchResultItem> Items {
            get {
                throw new NotImplementedException();
            }
            set {
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
            set{
            }
        }

        public DateTime Date {
            get {
                throw new NotImplementedException();
            }
            set {
            }
        }

        public string Identifier {
            get {
                throw new NotImplementedException();
            }
            set {
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

        public long TotalResults {
            get {
                throw new NotImplementedException();
            }
        }

        #endregion

        public static KmlOpenSearchResult CreateFromOpenSearchResultCollection(IOpenSearchResultCollection results) {
            Document doc = new Document();

            //find who call the search, shop cart or search response
            doc.Name = "ngEO Catalogue Search Response"; // or "ngEO Shop Cart"
            Description description = new Description();
            description.Text = doc.Name;
            doc.Description = description;
            SharpKml.Dom.Data queryTime = new SharpKml.Dom.Data();
            queryTime.Name = "queryTime";
            queryTime.DisplayName = "queryTime";
            queryTime.Value = results.ElementExtensions.ReadElementExtensions<string>("queryTime", "http://a9.com/-/spec/opensearch/1.1/")[0];
            doc.ExtendedData = new ExtendedData();
            doc.ExtendedData.AddData(queryTime);

            Style headerStyle = new Style();
            headerStyle.Id = "ngEO-balloon-style";
            BalloonStyle balloon = new BalloonStyle();

            balloon.Text = @"<![CDATA[
                            <style type=""text/css"">
                            .first_column
                            {
                            background-color:Lavender;
                            font-family:""Trebuchet MS"", Arial, Helvetica, sans-serif; font-weight:bold;
                            }
                            table
                            {  border-collapse:collapse; }
                            table, td, th
                            {
                            border:1px solid Grey; padding:5px;
                            }
                            </style> 
                            <TABLE>
                                <TR><TD class=""first_column"">Dataset</TD><TD>$[eop_parentIdentifier]</TD></TR>
                                <TR><TD class=""first_column"">ID</TD><TD>$[eop_identifier]</TD></TR>
                                <TR><TD class=""first_column"">Mission</TD><TD>$[eop_platformShortName]</TD></TR>
                                <TR><TD class=""first_column"">Satellite Number</TD><TD>$[eop_platformSerialIdentifier]</TD></TR>
                                <TR><TD class=""first_column"">Sensor</TD><TD>$[eop_instrumentShortName]</TD></TR>
                                <TR><TD class=""first_column"">Sensor Mode</TD><TD>$[eop_operationalMode]</TD></TR>
                                <TR><TD class=""first_column"">Product Type</TD><TD>$[eop_productType]</TD></TR>
                                <TR><TD class=""first_column"">Processing Mode</TD><TD>$[eop_processingMode]</TD></TR>
                                <TR><TD class=""first_column"">Product File Name</TD><TD>$[eop_ProductInformation.eop_filename]</TD></TR> 
                                <TR><TD class=""first_column"">Product Version</TD><TD>$[eop_ProductInformation.eop_version]</TD></TR> 
                                <TR><TD class=""first_column"">Product File Size</TD><TD>$[eop_ProductInformation.eop_size]</TD></TR>
                                <TR><TD class=""first_column"">Start Time</TD><TD>$[gml_beginPosition]</TD></TR>
                                <TR><TD class=""first_column"">Stop Time</TD><TD>$[gml_endPosition]</TD></TR>
                                <TR><TD class=""first_column"">Time Position</TD><TD>$[gml_timePosition]</TD></TR>  
                                <TR><TD class=""first_column"">Sensor Type</TD><TD>$[eop_sensorType]</TD></TR>
                                <TR><TD class=""first_column"">Swath</TD><TD>$[eop_swathIdentifer]</TD></TR>
                                <TR><TD class=""first_column"">Orbit</TD><TD>$[eop_orbitNumber]</TD></TR>
                                <TR><TD class=""first_column"">Pass Direction</TD><TD>$[eop_orbitDirection]</TD></TR>
                                <TR><TD class=""first_column"">WRS Longitude Grid</TD><TD>$[eop_wrsLongitudeGrid]</TD></TR>
                                <TR><TD class=""first_column"">WRS Latitude Grid</TD><TD>$[eop_wrsLatitudeGrid]</TD></TR>
                                <TR><TD class=""first_column"">ANX Start Time</TD><TD>$[eop_startTimeFromAscendingNode] ms</TD></TR> 
                                <TR><TD class=""first_column"">ANX Stop Time</TD><TD>$[eop_completionTimeFromAscendingNode] ms</TD></TR> 
                                <TR><TD class=""first_column"">Product Status</TD><TD>$[eop_status]</TD></TR>
                                <TR><TD class=""first_column"">Polarisation Mode</TD><TD>$[sar_polarisationMode]</TD></TR>
                                <TR><TD class=""first_column"">Polarisation Channels</TD><TD>$[sar_polarisationChannels]</TD></TR>
                                <TR><TD class=""first_column"">Cloud Coverage</TD><TD>$[atm_cloudCoverPercentage]%</TD></TR>
                                <TR><TD class=""first_column"">Snow Coverage</TD><TD>$[atm_snowCoverPercentage]%</TD></TR>
                                <TR><TD class=""first_column"">Cloud Coverage</TD><TD>$[opt_cloudCoverPercentage]</TD></TR>
                                <TR><TD class=""first_column"">Snow Coverage</TD><TD>$[opt_snowCoverPercentage]</TD></TR>
                                <TR><TD class=""first_column"">Illumination Azimuth Angle</TD><TD>$[eop_illuminationAzimuthAngle] deg</TD></TR> 
                                <TR><TD class=""first_column"">Illumination Zenith Angle</TD><TD>$[eop_illuminationZenithAngle] deg</TD></TR>
                                <TR><TD class=""first_column"">Illumination Elevation Angle</TD><TD>$[eop_illuminationElevationAngle] deg</TD></TR> 
                                <TR><TD class=""first_column"">Multi View Angles</TD><TD>$[atm_multiViewAngles] deg</TD></TR>
                                <TR><TD class=""first_column"">Centre View Angles</TD><TD>$[atm_centreViewAngles] deg</TD></TR>
                                <TR><TD class=""first_column"">Antenna Look Direction</TD><TD>$[sar_antennaLookDirection]</TD></TR>
                                <TR><TD class=""first_column"">Minimum Incidence Angle</TD><TD>$[sar_minimumIncidenceAngle] deg</TD></TR> 
                                <TR><TD class=""first_column"">Maximum Incidence Angle</TD><TD>$[sar_maximumIncidenceAngle] deg</TD></TR>
                                <TR><TD class=""first_column"">Incidence Angle Variation</TD><TD>$[sar_incidenceAngleVariation]</TD></TR>
                                <TR><TD class=""first_column"">Doppler Frequency</TD><TD>$[sar_dopplerFrequency]</TD></TR>
                                <TR><TD class=""first_column"">Observed Property</TD><TD>$[om_observedProperty]</TD></TR>
                                <TR><TD class=""first_column"">Acquisition Type</TD><TD>$[eop_acquisitionType]</TD></TR>
                                <TR><TD class=""first_column"">Acquisition Sub Type</TD><TD>$[eop_acquisitionSubType]</TD></TR>
                                <TR><TD class=""first_column"">Image Quality Degradation</TD><TD>$[eop_imageQualityDegradation]</TD></TR> 
                                <TR><TD class=""first_column"">Image Quality Status</TD><TD>$[eop_imageQualityStatus]</TD></TR>
                                <TR><TD class=""first_column"">Image Quality Degradation Tag</TD><TD>$[eop_imageQualityDegradationTag]</TD></TR>
                                <TR><TD class=""first_column"">Image Quality Report URL</TD><TD>$[eop_imageQualityReportURL]</TD></TR> 
                                <TR><TD class=""first_column"">Group ID</TD><TD>$[eop_productGroupId]</TD></TR>
                                <TR><TD class=""first_column"">Local Attribute</TD><TD>$[eop_localAttribute]</TD></TR>
                                <TR><TD class=""first_column"">Local Value</TD><TD>$[eop_localValue]</TD></TR>
                                <TR><TD class=""first_column"">Browse URL</TD><TD><A href=""$[eop_BrowseInformation.eop_filename]"">BrowseImage</A></TD></TR> 
                            </TABLE>
                            ]]>";

            headerStyle.Balloon = balloon;
            doc.AddStyle(headerStyle);

            LineStyle line = new LineStyle();
            line.Width = 2.0;
            line.Color = Color32.Parse("ff0000ff");

            PolygonStyle polygonStyle = new PolygonStyle();
            polygonStyle.Fill = true;
            polygonStyle.ColorMode = ColorMode.Normal;
            polygonStyle.Color = Color32.Parse("440000ff");


            Style style = new Style();
            style.Polygon = polygonStyle;
            style.Line = line;


            foreach (IOpenSearchResultItem item in results.Items) {

                EarthObservation earthObservation = GetEarthObservation(item);

                Placemark placeMark = new Placemark();
                placeMark.Name = earthObservation.EarthObservationMetaData.Identifier;
                placeMark.StyleUrl = new Uri("#ngEO-balloon-style", UriKind.Relative);

                // retrive the single earthObservation node information
                ExtendedData eData = new ExtendedData();

                SharpKml.Dom.Data gml_beginPositionData = new SharpKml.Dom.Data();
                gml_beginPositionData.Name = "gml_beginPosition";
                gml_beginPositionData.Value = earthObservation.BeginPosition;
                eData.AddData(gml_beginPositionData);

                SharpKml.Dom.Data gml_endPositionData = new SharpKml.Dom.Data();
                gml_endPositionData.Name = "gml_endPosition";
                gml_endPositionData.Value = earthObservation.EndPosition;
                eData.AddData(gml_endPositionData);

                SharpKml.Dom.Data gml_timePositionData = new SharpKml.Dom.Data();
                gml_timePositionData.Name = "gml_timePosition";
                gml_timePositionData.Value = earthObservation.TimePosition;
                eData.AddData(gml_timePositionData);


                // testing not null EarthObservationEquipment node
                if (earthObservation.EarthObservationEquipment != null) {
                    try {

                        SharpKml.Dom.Data eop_platformShortNameData = new SharpKml.Dom.Data();
                        eop_platformShortNameData.Name = "eop_platformShortName";
                        eop_platformShortNameData.Value = earthObservation.EarthObservationEquipment.PlatformShortName;
                        eData.AddData(eop_platformShortNameData);

                        SharpKml.Dom.Data eop_platformSerialIdentifierData = new SharpKml.Dom.Data();
                        eop_platformSerialIdentifierData.Name = "eop_platformSerialIdentifier";
                        eop_platformSerialIdentifierData.Value = earthObservation.EarthObservationEquipment.PlatformSerialIdentifier;
                        eData.AddData(eop_platformSerialIdentifierData);

                        SharpKml.Dom.Data eop_instrumentShortNameData = new SharpKml.Dom.Data();
                        eop_instrumentShortNameData.Name = "eop_instrumentShortName";
                        eop_instrumentShortNameData.Value = earthObservation.EarthObservationEquipment.InstrumentShortName;
                        eData.AddData(eop_instrumentShortNameData);

                        SharpKml.Dom.Data eop_sensorTypeData = new SharpKml.Dom.Data();
                        eop_sensorTypeData.Name = "eop_sensorType";
                        eop_sensorTypeData.Value = earthObservation.EarthObservationEquipment.SensorType;
                        eData.AddData(eop_sensorTypeData);

                        SharpKml.Dom.Data eop_operationalModeData = new SharpKml.Dom.Data();
                        eop_operationalModeData.Name = "eop_operationalMode";
                        eop_operationalModeData.Value = earthObservation.EarthObservationEquipment.OperationalMode;
                        eData.AddData(eop_operationalModeData);

                        SharpKml.Dom.Data eop_swathIdentiferData = new SharpKml.Dom.Data();
                        eop_swathIdentiferData.Name = "eop_swathIdentifer";
                        eop_swathIdentiferData.Value = earthObservation.EarthObservationEquipment.SwathIdentifier;
                        eData.AddData(eop_swathIdentiferData);

                        // testing not null Acquisition node
                        if (earthObservation.EarthObservationEquipment.Acquisition != null) {

                            try {
                                SharpKml.Dom.Data eop_orbitNumberData = new SharpKml.Dom.Data();
                                eop_orbitNumberData.Name = "eop_orbitNumber";
                                eop_orbitNumberData.Value = earthObservation.EarthObservationEquipment.Acquisition.OrbitNumber.ToString();
                                eData.AddData(eop_orbitNumberData);

                                SharpKml.Dom.Data eop_orbitDirectionData = new SharpKml.Dom.Data();
                                eop_orbitDirectionData.Name = "eop_orbitDirection";
                                eop_orbitDirectionData.Value = earthObservation.EarthObservationEquipment.Acquisition.OrbitDirection;
                                eData.AddData(eop_orbitDirectionData);

                                SharpKml.Dom.Data eop_wrsLongitudeGridData = new SharpKml.Dom.Data();
                                eop_wrsLongitudeGridData.Name = "eop_wrsLongitudeGrid";
                                eop_wrsLongitudeGridData.Value = earthObservation.EarthObservationEquipment.Acquisition.WrsLongitudeGrid;
                                eData.AddData(eop_wrsLongitudeGridData);

                                SharpKml.Dom.Data eop_wrsLatitudeGridData = new SharpKml.Dom.Data();
                                eop_wrsLatitudeGridData.Name = "eop_wrsLatitudeGrid";
                                eop_wrsLatitudeGridData.Value = earthObservation.EarthObservationEquipment.Acquisition.WrsLatitudeGrid;
                                eData.AddData(eop_wrsLatitudeGridData);

                                SharpKml.Dom.Data eop_startTimeFromAscendingNodeData = new SharpKml.Dom.Data();
                                eop_startTimeFromAscendingNodeData.Name = "eop_startTimeFromAscendingNode";
                                eop_startTimeFromAscendingNodeData.Value = earthObservation.EarthObservationEquipment.Acquisition.StartTimeFromAscendingNode.ToString();
                                eData.AddData(eop_startTimeFromAscendingNodeData);

                                SharpKml.Dom.Data eop_completionTimeFromAscendingNodeData = new SharpKml.Dom.Data();
                                eop_completionTimeFromAscendingNodeData.Name = "eop_completionTimeFromAscendingNode";
                                eop_completionTimeFromAscendingNodeData.Value = earthObservation.EarthObservationEquipment.Acquisition.CompletionTimeFromAscendingNode.ToString();
                                eData.AddData(eop_completionTimeFromAscendingNodeData);

                                SharpKml.Dom.Data eop_illuminationAzimuthAngleData = new SharpKml.Dom.Data();
                                eop_illuminationAzimuthAngleData.Name = "eop_illuminationAzimuthAngle";
                                eop_illuminationAzimuthAngleData.Value = earthObservation.EarthObservationEquipment.Acquisition.IlluminationAzimuthAngle.ToString();
                                eData.AddData(eop_illuminationAzimuthAngleData);

                                SharpKml.Dom.Data eop_illuminationZenithAngleData = new SharpKml.Dom.Data();
                                eop_illuminationZenithAngleData.Name = "eop_illuminationZenithAngle";
                                eop_illuminationZenithAngleData.Value = earthObservation.EarthObservationEquipment.Acquisition.IlluminationZenithAngle.ToString();
                                eData.AddData(eop_illuminationZenithAngleData);

                                SharpKml.Dom.Data eop_illuminationElevationAngleData = new SharpKml.Dom.Data();
                                eop_illuminationElevationAngleData.Name = "eop_illuminationElevationAngle";
                                eop_illuminationElevationAngleData.Value = earthObservation.EarthObservationEquipment.Acquisition.IlluminationElevationAngle.ToString();
                                eData.AddData(eop_illuminationElevationAngleData);

                                SharpKml.Dom.Data atm_multiViewAnglesData = new SharpKml.Dom.Data();
                                atm_multiViewAnglesData.Name = "atm_multiViewAngles";
                                atm_multiViewAnglesData.Value = earthObservation.EarthObservationEquipment.Acquisition.MultiViewAngles.ToString();
                                eData.AddData(atm_multiViewAnglesData);

                                SharpKml.Dom.Data atm_centreViewAnglesData = new SharpKml.Dom.Data();
                                atm_centreViewAnglesData.Name = "atm_centreViewAngles";
                                atm_centreViewAnglesData.Value = earthObservation.EarthObservationEquipment.Acquisition.CentreViewAngles.ToString();
                                eData.AddData(atm_centreViewAnglesData);

                                SharpKml.Dom.Data sar_polarisationModeData = new SharpKml.Dom.Data();
                                sar_polarisationModeData.Name = "sar_polarisationMode";
                                sar_polarisationModeData.Value = earthObservation.EarthObservationEquipment.Acquisition.PolarisationMode;
                                eData.AddData(sar_polarisationModeData);

                                SharpKml.Dom.Data sar_polarisationChannelsData = new SharpKml.Dom.Data();
                                sar_polarisationChannelsData.Name = "sar_polarisationChannels";
                                sar_polarisationChannelsData.Value = earthObservation.EarthObservationEquipment.Acquisition.PolarisationChannels;
                                eData.AddData(sar_polarisationChannelsData);

                                SharpKml.Dom.Data sar_antennaLookDirectionData = new SharpKml.Dom.Data();
                                sar_antennaLookDirectionData.Name = "sar_antennaLookDirection";
                                sar_antennaLookDirectionData.Value = earthObservation.EarthObservationEquipment.Acquisition.AntennaLookDirection;
                                eData.AddData(sar_antennaLookDirectionData);

                                SharpKml.Dom.Data sar_minimumIncidenceAngleData = new SharpKml.Dom.Data();
                                sar_minimumIncidenceAngleData.Name = "sar_minimumIncidenceAngle";
                                sar_minimumIncidenceAngleData.Value = earthObservation.EarthObservationEquipment.Acquisition.MinimumIncidenceAngle.ToString();
                                eData.AddData(sar_minimumIncidenceAngleData);

                                SharpKml.Dom.Data sar_maximumIncidenceAngleData = new SharpKml.Dom.Data();
                                sar_maximumIncidenceAngleData.Name = "sar_maximumIncidenceAngle";
                                sar_maximumIncidenceAngleData.Value = earthObservation.EarthObservationEquipment.Acquisition.MaximumIncidenceAngle.ToString();
                                eData.AddData(sar_maximumIncidenceAngleData);

                                SharpKml.Dom.Data sar_incidenceAngleVariationData = new SharpKml.Dom.Data();
                                sar_incidenceAngleVariationData.Name = "sar_incidenceAngleVariation";
                                sar_incidenceAngleVariationData.Value = earthObservation.EarthObservationEquipment.Acquisition.IncidenceAngleVariation.ToString();
                                eData.AddData(sar_incidenceAngleVariationData);

                                SharpKml.Dom.Data sar_dopplerFrequencyData = new SharpKml.Dom.Data();
                                sar_dopplerFrequencyData.Name = "sar_dopplerFrequency";
                                sar_dopplerFrequencyData.Value = earthObservation.EarthObservationEquipment.Acquisition.DopplerFrequency.ToString();
                                eData.AddData(sar_dopplerFrequencyData);
                            } catch (Exception e) {
                                // TODO solve the log problem
                                //Terradue.Portal.Logger.Error(this, e.Message);
                            }

                        }
                    } catch (Exception) {
                        // TODO solve the log problem
                        //Terradue.Portal.Logger.Error(this, "earthObservation.EarthObservationEquipment");
                    }
                }

                SharpKml.Dom.Data om_observedPropertyData = new SharpKml.Dom.Data();
                om_observedPropertyData.Name = "om_observedProperty";
                om_observedPropertyData.Value = earthObservation.ObservedProperty;
                eData.AddData(om_observedPropertyData);

                // testing not null EarthObservationResult node
                if (earthObservation.EarthObservationResult != null) {

                    // testing not null BrowseInformation node
                    if (earthObservation.EarthObservationResult.BrowseInformation != null) {

                        SharpKml.Dom.Data eop_BrowseInformationeop_filenameData = new SharpKml.Dom.Data();
                        eop_BrowseInformationeop_filenameData.Name = "eop_BrowseInformation.eop_filename";
                        eop_BrowseInformationeop_filenameData.Value = earthObservation.EarthObservationResult.BrowseInformation.Url;
                        eData.AddData(eop_BrowseInformationeop_filenameData);

                    }

                    // testing not null ProductInformation node
                    if (earthObservation.EarthObservationResult.ProductInformation != null) {

                        SharpKml.Dom.Data eop_ProductInformationeop_filenameData = new SharpKml.Dom.Data();
                        eop_ProductInformationeop_filenameData.Name = "eop_ProductInformation.eop_filename";
                        eop_ProductInformationeop_filenameData.Value = earthObservation.EarthObservationResult.ProductInformation.Filename;
                        eData.AddData(eop_ProductInformationeop_filenameData);

                        SharpKml.Dom.Data eop_ProductInformationeop_versionData = new SharpKml.Dom.Data();
                        eop_ProductInformationeop_versionData.Name = "eop_ProductInformation.eop_version";
                        eop_ProductInformationeop_versionData.Value = earthObservation.EarthObservationResult.ProductInformation.Version;
                        eData.AddData(eop_ProductInformationeop_versionData);

                        SharpKml.Dom.Data eop_ProductInformationeop_sizeData = new SharpKml.Dom.Data();
                        eop_ProductInformationeop_sizeData.Name = "eop_ProductInformation.eop_size";
                        eop_ProductInformationeop_sizeData.Value = earthObservation.EarthObservationResult.ProductInformation.Size.ToString();
                        eData.AddData(eop_ProductInformationeop_sizeData);
                    }

                    SharpKml.Dom.Data atm_cloudCoverPercentageData = new SharpKml.Dom.Data();
                    atm_cloudCoverPercentageData.Name = "atm_cloudCoverPercentage";
                    atm_cloudCoverPercentageData.Value = earthObservation.EarthObservationResult.CloudCoverPercentage.ToString();
                    eData.AddData(atm_cloudCoverPercentageData);

                    SharpKml.Dom.Data atm_snowCoverPercentageData = new SharpKml.Dom.Data();
                    atm_snowCoverPercentageData.Name = "atm_snowCoverPercentage";
                    atm_snowCoverPercentageData.Value = earthObservation.EarthObservationResult.SnowCoverPercentage.ToString();
                    eData.AddData(atm_snowCoverPercentageData);

                    SharpKml.Dom.Data opt_cloudCoverPercentageData = new SharpKml.Dom.Data();
                    opt_cloudCoverPercentageData.Name = "opt_cloudCoverPercentage";
                    opt_cloudCoverPercentageData.Value = earthObservation.EarthObservationResult.CloudCoverPercentage1.ToString();
                    eData.AddData(opt_cloudCoverPercentageData);

                    SharpKml.Dom.Data opt_snowCoverPercentageData = new SharpKml.Dom.Data();
                    opt_snowCoverPercentageData.Name = "opt_snowCoverPercentage";
                    opt_snowCoverPercentageData.Value = earthObservation.EarthObservationResult.SnowCoverPercentage1.ToString();
                    eData.AddData(opt_snowCoverPercentageData);
                }

                // testing not null EarthObservationMetaData node
                if (earthObservation.EarthObservationMetaData != null) {

                    SharpKml.Dom.Data eop_identifierData = new SharpKml.Dom.Data();
                    eop_identifierData.Name = "eop_identifier";
                    eop_identifierData.Value = earthObservation.EarthObservationMetaData.Identifier;
                    eData.AddData(eop_identifierData);

                    SharpKml.Dom.Data eop_parentIdentifierData = new SharpKml.Dom.Data();
                    eop_parentIdentifierData.Name = "eop_parentIdentifier";
                    eop_parentIdentifierData.Value = earthObservation.EarthObservationMetaData.ParentIdentifier;
                    eData.AddData(eop_parentIdentifierData);

                    SharpKml.Dom.Data eop_acquisitionTypeData = new SharpKml.Dom.Data();
                    eop_acquisitionTypeData.Name = "eop_acquisitionType";
                    eop_acquisitionTypeData.Value = earthObservation.EarthObservationMetaData.AcquisitionType;
                    eData.AddData(eop_acquisitionTypeData);

                    SharpKml.Dom.Data eop_acquisitionSubTypeData = new SharpKml.Dom.Data();
                    eop_acquisitionSubTypeData.Name = "eop_acquisitionSubType";
                    eop_acquisitionSubTypeData.Value = earthObservation.EarthObservationMetaData.AcquisitionSubType;
                    eData.AddData(eop_acquisitionSubTypeData);

                    SharpKml.Dom.Data eop_productTypeData = new SharpKml.Dom.Data();
                    eop_productTypeData.Name = "eop_productType";
                    eop_productTypeData.Value = earthObservation.EarthObservationMetaData.ProductType;
                    eData.AddData(eop_productTypeData);

                    SharpKml.Dom.Data eop_statusData = new SharpKml.Dom.Data();
                    eop_statusData.Name = "eop_status";
                    eop_statusData.Value = earthObservation.EarthObservationMetaData.Status;
                    eData.AddData(eop_statusData);

                    SharpKml.Dom.Data eop_imageQualityDegradationData = new SharpKml.Dom.Data();
                    eop_imageQualityDegradationData.Name = "eop_imageQualityDegradation";
                    eop_imageQualityDegradationData.Value = earthObservation.EarthObservationMetaData.ImageQualityDegradation;
                    eData.AddData(eop_imageQualityDegradationData);

                    SharpKml.Dom.Data eop_imageQualityStatusData = new SharpKml.Dom.Data();
                    eop_imageQualityStatusData.Name = "eop_imageQualityStatus";
                    eop_imageQualityStatusData.Value = earthObservation.EarthObservationMetaData.ImageQualityStatus;
                    eData.AddData(eop_imageQualityStatusData);

                    SharpKml.Dom.Data eop_imageQualityDegradationTagData = new SharpKml.Dom.Data();
                    eop_imageQualityDegradationTagData.Name = "eop_imageQualityDegradationTag";
                    eop_imageQualityDegradationTagData.Value = earthObservation.EarthObservationMetaData.ImageQualityDegradationTag;
                    eData.AddData(eop_imageQualityDegradationTagData);

                    SharpKml.Dom.Data eop_imageQualityReportURLData = new SharpKml.Dom.Data();
                    eop_imageQualityReportURLData.Name = "eop_imageQualityReportURL";
                    eop_imageQualityReportURLData.Value = earthObservation.EarthObservationMetaData.ImageQualityReportURL;
                    eData.AddData(eop_imageQualityReportURLData);

                    // testing not null EarthObservationMetaData node
                    if (earthObservation.EarthObservationMetaData.Processing != null) {

                        SharpKml.Dom.Data eop_processingModeData = new SharpKml.Dom.Data();
                        eop_processingModeData.Name = "eop_processingMode";
                        eop_processingModeData.Value = earthObservation.EarthObservationMetaData.Processing.ProcessingMode;
                        eData.AddData(eop_processingModeData);
                    }

                    SharpKml.Dom.Data eop_productGroupIdData = new SharpKml.Dom.Data();
                    eop_productGroupIdData.Name = "eop_productGroupId";
                    eop_productGroupIdData.Value = earthObservation.EarthObservationMetaData.ProductGroupId;
                    eData.AddData(eop_productGroupIdData);

                    SharpKml.Dom.Data eop_localAttributeData = new SharpKml.Dom.Data();
                    eop_localAttributeData.Name = "eop_localAttribute";
                    StringBuilder sBuilder = new StringBuilder();
                    if (earthObservation.EarthObservationMetaData.LocalAttribute != null) {

                        if (earthObservation.EarthObservationMetaData.LocalAttribute.Count > 0) {
                            foreach (string attr in earthObservation.EarthObservationMetaData.LocalAttribute) sBuilder.Append(attr + " ");

                            eop_localAttributeData.Value = sBuilder.ToString().Trim();
                        } else {
                            eop_localAttributeData.Value = null;
                        }
                    } else eop_localAttributeData.Value = null;

                    eData.AddData(eop_localAttributeData);

                    SharpKml.Dom.Data eop_localValueData = new SharpKml.Dom.Data();
                    eop_localValueData.Name = "eop_localValue";
                    sBuilder.Clear();

                    if (earthObservation.EarthObservationMetaData.LocalValue != null) {

                        if (earthObservation.EarthObservationMetaData.LocalValue.Count > 0) {
                            foreach (string attr in earthObservation.EarthObservationMetaData.LocalValue) sBuilder.Append(attr + " ");

                            eop_localValueData.Value = sBuilder.ToString().Trim();
                        } else {
                            eop_localValueData.Value = null;
                        }
                    } else eop_localValueData.Value = null;

                    eData.AddData(eop_localValueData);
                }

                // building the footprint
                XmlDocument coordinatesDoc = new XmlDocument();

                foreach (SyndicationElementExtension ext in item.ElementExtensions) {
                    if (ext.OuterName == "EarthObservation") {
                        coordinatesDoc.Load(ext.GetReader());
                        // found the item i need, exit from loop
                        break;
                    }
                }



                XmlNamespaceManager xnsm = new XmlNamespaceManager(new XmlDocument().NameTable);
                xnsm.AddNamespace("om", "http://www.opengis.net/om/2.0");
                xnsm.AddNamespace("eop", "http://www.opengis.net/eop/2.0");
                xnsm.AddNamespace("sar", "http://www.opengis.net/sar/2.0");
                xnsm.AddNamespace("ows", "http://www.opengis.net/ows/2.0");
                xnsm.AddNamespace("gml", "http://www.opengis.net/gml/3.2");

                // before calling gmlCheckVer3dot2 we must add "http://www.opengis.net/gml/3.2"
                // then, call gmlCheckVer3dot2 to check version. If false then add gml v1 
                // to the xml namspace  
                COORDINATE coordinateSystem = COORDINATE.LATLONG; // default value for gml 3.2 standard
                if (!gmlCheckVer3dot2(coordinatesDoc.DocumentElement.SelectSingleNode("om:featureOfInterest/eop:Footprint/eop:multiExtentOf", xnsm), xnsm)) {
                    xnsm.AddNamespace("gml", "http://www.opengis.net/gml");
                    coordinateSystem = COORDINATE.LONGLAT; // default value for gml 2.0 standard
                }
                placeMark.Geometry = KMLGeometry(coordinatesDoc.DocumentElement.SelectSingleNode("om:featureOfInterest/eop:Footprint/eop:multiExtentOf", xnsm), xnsm, coordinateSystem);

                // assigning values to the placemark
                placeMark.AddStyle(style);
                placeMark.ExtendedData = eData;

                //adding the placemark to the document
                doc.AddFeature(placeMark);
            }

            KmlOpenSearchResult kmlResult = new KmlOpenSearchResult();
            kmlResult.Kml.Feature = doc;

            return kmlResult;
        }

        public static EarthObservation GetEarthObservation(IOpenSearchResultItem entry) {
            EarthObservation earthObservation = null;

            foreach (SyndicationElementExtension ext in entry.ElementExtensions) {

                if (ext.OuterName == "EarthObservation") {

                    XmlDocument doc = new XmlDocument();
                    doc.Load(ext.GetReader());
                    XmlElement eop = doc.DocumentElement;

                    earthObservation = FromXmlElement(eop);

                    return earthObservation;

                }

            }
            return earthObservation;
        }

        public static EarthObservation FromXmlElement(XmlElement eop) {

            EarthObservation earthObservation = new EarthObservation();
            XmlNodeList nl;
            XmlNode el;

            XmlNamespaceManager xnsm = new XmlNamespaceManager(new XmlDocument().NameTable);
            xnsm.AddNamespace("om", "http://www.opengis.net/om/2.0");
            xnsm.AddNamespace("gml", "http://www.opengis.net/gml/3.2");
            xnsm.AddNamespace("eop", "http://www.opengis.net/eop/2.0");
            xnsm.AddNamespace("sar", "http://www.opengis.net/sar/2.0");
            xnsm.AddNamespace("ows", "http://www.opengis.net/ows/2.0");

            el = eop.SelectSingleNode("om:phenomenonTime/gml:TimePeriod/gml:beginPosition", xnsm);
            if (el != null) earthObservation.BeginPosition = el.InnerText;

            el = eop.SelectSingleNode("om:phenomenonTime/gml:TimePeriod/gml:endPosition", xnsm);
            if (el != null) earthObservation.EndPosition = el.InnerText;

            el = eop.SelectSingleNode("om:resultTime/ gml:TimeInstant/gml:timePosition", xnsm);
            if (el != null) earthObservation.TimePosition = el.InnerText;

            el = eop.SelectSingleNode("om:observedProperty", xnsm);
            if (el != null) earthObservation.ObservedProperty = el.Attributes["nilReason"].Value;

            earthObservation.EarthObservationEquipment = new EarthObservationEquipment();

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:platform/eop:Platform/eop:shortName", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.PlatformShortName = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:platform/eop:Platform/eop:serialIdentifier", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.PlatformSerialIdentifier = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:instrument/eop:Instrument/eop:shortName", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.InstrumentShortName = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:sensor/eop:Sensor/eop:sensorType", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.SensorType = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:sensor/eop:Sensor/eop:operationalMode", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.OperationalMode = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:sensor/eop:Sensor/eop:swathIdentifier", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.SwathIdentifier = el.InnerText;

            earthObservation.EarthObservationEquipment.Acquisition = new Acquisition();

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:orbitNumber", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.OrbitNumber = uint.Parse(el.InnerText);

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:orbitDirection", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.OrbitDirection = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:wrsLongitudeGrid", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.WrsLongitudeGrid = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:wrsLatitudeGrid", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.WrsLatitudeGrid = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:startTimeFromAscendingNode", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.StartTimeFromAscendingNode = int.Parse(el.InnerText);

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:completionTimeFromAscendingNode", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.CompletionTimeFromAscendingNode = int.Parse(el.InnerText);

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:illuminationAzimuthAngle", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.IlluminationAzimuthAngle = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:illuminationZenithAngle", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.IlluminationZenithAngle = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/eop:illuminationElevationAngle", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.IlluminationElevationAngle = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/atm:multiViewAngles", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.MultiViewAngles = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/atm:centreViewAngles", xnsm);
            if (el != null && el.InnerText != "") earthObservation.EarthObservationEquipment.Acquisition.CentreViewAngles = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:polarisationMode", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.PolarisationMode = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:polarisationChannels", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.PolarisationChannels = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:antennaLookDirection", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.AntennaLookDirection = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:minimumIncidenceAngle", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.MinimumIncidenceAngle = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:maximumIncidenceAngle", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.MaximumIncidenceAngle = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:incidenceAngleVariation", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.IncidenceAngleVariation = el.InnerText;

            el = eop.SelectSingleNode("om:procedure/eop:EarthObservationEquipment/eop:acquisitionParameters/*[local-name()='Acquisition']/sar:dopplerFrequency", xnsm);
            if (el != null) earthObservation.EarthObservationEquipment.Acquisition.DopplerFrequency = el.InnerText;

            earthObservation.EarthObservationMetaData = new EarthObservationMetaData();

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:identifier", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.Identifier = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:parentIdentifier", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ParentIdentifier = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:acquisitionType", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.AcquisitionType = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:acquisitionSubType", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.AcquisitionSubType = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:productType", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ProductType = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:status", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.Status = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:imageQualityDegradation", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ImageQualityDegradation = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:imageQualityStatus", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ImageQualityStatus = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:imageQualityDegradationTag", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ImageQualityDegradationTag = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:imageQualityReportURL", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ImageQualityReportURL = el.InnerText;

            el = eop.SelectSingleNode("eop:metaDataProperty/*[local-name() = 'EarthObservationMetaData']/eop:productGroupId", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.ProductGroupId = el.InnerText;

            earthObservation.EarthObservationMetaData.Processing = new Processing();
            el = eop.SelectSingleNode("eop:metaDataProperty/eop:EarthObservationMetaData/eop:processing/*[local-name() = 'ProcessingInformation']/eop:processingMode", xnsm);
            if (el != null) earthObservation.EarthObservationMetaData.Processing.ProcessingMode = el.InnerText;

            earthObservation.EarthObservationMetaData.LocalAttribute = new List<string>();
            nl = eop.SelectNodes("eop:metaDataProperty/eop:EarthObservationMetaData/eop:vendorSpecific/eop:SpecificInformation/eop:localAttribute", xnsm);
            foreach (XmlNode xNode in nl) earthObservation.EarthObservationMetaData.LocalAttribute.Add(xNode.InnerText);

            earthObservation.EarthObservationMetaData.LocalValue = new List<string>();
            nl = eop.SelectNodes("eop:metaDataProperty/eop:EarthObservationMetaData/eop:vendorSpecific/eop:SpecificInformation/eop:localValue", xnsm);
            foreach (XmlNode xNode in nl) earthObservation.EarthObservationMetaData.LocalValue.Add(xNode.InnerText);

            earthObservation.EarthObservationResult = new EarthObservationResult();

            el = eop.SelectSingleNode("om:result/eop:EarthObservationResult/atm:cloudCoverPercentage", xnsm);
            if (el != null) earthObservation.EarthObservationResult.CloudCoverPercentage = Convert.ToDouble(el.InnerText);      

            el = eop.SelectSingleNode("om:result/eop:EarthObservationResult/atm:snowCoverPercentage", xnsm);
            if (el != null) earthObservation.EarthObservationResult.SnowCoverPercentage = Convert.ToUInt16(el.InnerText);

            el = eop.SelectSingleNode("om:result/eop:EarthObservationResult/opt:cloudCoverPercentage", xnsm);
            if (el != null) earthObservation.EarthObservationResult.CloudCoverPercentage1 = Convert.ToUInt16(el.InnerText);

            el = eop.SelectSingleNode("om:result/eop:EarthObservationResult/opt:snowCoverPercentage", xnsm);
            if (el != null) earthObservation.EarthObservationResult.SnowCoverPercentage1 = Convert.ToUInt16(el.InnerText);

            earthObservation.EarthObservationResult.ProductInformation = new ProductInformation();

            el = eop.SelectSingleNode("om:result/*[local-name() = 'EarthObservationResult']/eop:product/eop:ProductInformation/eop:fileName/ows:ServiceReference", xnsm);
            if (el != null) earthObservation.EarthObservationResult.ProductInformation.Filename = el.Attributes["href", "http://www.w3.org/1999/xlink"].Value;

            el = eop.SelectSingleNode("om:result/*[local-name() = 'EarthObservationResult']/eop:product/eop:ProductInformation/eop:vesion", xnsm);
            if (el != null) earthObservation.EarthObservationResult.ProductInformation.Version = el.InnerText;

            el = eop.SelectSingleNode("om:result/*[local-name() = 'EarthObservationResult']/eop:product/eop:ProductInformation/eop:size", xnsm);
            if (el != null) earthObservation.EarthObservationResult.ProductInformation.Size = Convert.ToInt32(el.InnerText);

            earthObservation.EarthObservationResult.BrowseInformation = new BrowseInformation();
            // to do!!!
            el = eop.SelectSingleNode("om:result/*[local-name() = 'EarthObservationResult']/eop:browse/eop:BrowseInformation/eop:fileName/ows:ServiceReference", xnsm);
            if (el != null) earthObservation.EarthObservationResult.BrowseInformation.Url = el.Attributes["href", "http://www.w3.org/1999/xlink"].Value;

            return earthObservation;
        }

        // find the 2d/3d polygon's vertex coordinates
        public static CoordinateCollection Coordinates(string coordinatesString, EDIMENSION dimension, COORDINATE coordinateSystem) {

            CoordinateCollection coordinates = new CoordinateCollection();
            string[] dot = coordinatesString.Split(' ');

            switch (dimension) {
                case EDIMENSION.TWO:
                    // 3d coordinates [long , lat , high = 0 ]
                    for (int i = 0; i < dot.Length / 2; i++) if (coordinateSystem == COORDINATE.LONGLAT) coordinates.Add(new Vector(Convert.ToDouble(dot[2 * i]), Convert.ToDouble(dot[2 * i + 1]), 0));
                    else coordinates.Add(new Vector(Convert.ToDouble(dot[2 * i + 1]), Convert.ToDouble(dot[2 * i]), 0));
                    break;
                case EDIMENSION.THREE:
                    // 3d coordinates [long , lat , high ]
                    for (int i = 0; i < dot.Length / 3; i++) if (coordinateSystem == COORDINATE.LONGLAT) coordinates.Add(new Vector(Convert.ToDouble(dot[3 * i]), Convert.ToDouble(dot[3 * i + 1]), Convert.ToDouble(dot[3 * i + 2])));
                    else coordinates.Add(new Vector(Convert.ToDouble(dot[3 * i + 1]), Convert.ToDouble(dot[3 * i]), Convert.ToDouble(dot[3 * i + 2])));
                    break;
            }

            return coordinates;

        }
        // find the 2d/3d points coordinates
        public static Vector PointCoordinates(string coordinatesString, EDIMENSION dimension, COORDINATE coordinateSystem) {
            string[] dot = coordinatesString.Split(' ');
            Vector vector = null;

            if (coordinateSystem == COORDINATE.LONGLAT) vector = new Vector(Convert.ToDouble(dot[0]), Convert.ToDouble(dot[1]), 0);
            else vector = new Vector(Convert.ToDouble(dot[1]), Convert.ToDouble(dot[0]), 0);

            if (dimension == EDIMENSION.THREE) vector.Altitude = Convert.ToDouble(dot[2]);

            return vector;
        }

        public static bool gmlCheckVer3dot2(XmlNode node, XmlNamespaceManager xnsm) {
            bool gmlNameSpaceFound = false;

            // recognizing version
            foreach (string item in Enum.GetNames(typeof(EGEOMETRYTYPE))) {
                if (node.SelectSingleNode("gml:" + item, xnsm) != null) {
                    gmlNameSpaceFound = true;
                    break;
                }
            }   
            return gmlNameSpaceFound;
        }
        // read GML geometry type and return the correspondending kml polygon
        public static Geometry KMLGeometry(XmlNode node, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            Geometry geometry = null;
            EGEOMETRYTYPE geoType = EGEOMETRYTYPE.NONE; 
            // checking the srsName to get the coordinate system (always EPSG:4326 ?)
            // Then we must convert the taken coordinates from source coordinate system 
            // to the target coordinate system

            // check the geometry type
            foreach (string item in Enum.GetNames(typeof(EGEOMETRYTYPE))) {
                if (node.SelectSingleNode("gml:" + item, xnsm) != null) {
                    geoType = (EGEOMETRYTYPE)Enum.Parse(typeof(EGEOMETRYTYPE), item);
                    break;
                }
            }   

            switch (geoType) {
                case EGEOMETRYTYPE.Polygon:
                    // a Polygon is a Surface, maybe we always get a polygon  instead of a gml:surface
                    geometry = getSurface(node.SelectSingleNode("gml:Polygon", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.Point:
                    geometry = getPoint(node.SelectSingleNode("gml:Point", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.MultiPoint:
                    geometry = getMultiPoint(node.SelectSingleNode("gml:MultiPoint", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.Surface:
                    geometry = getSurface(node.SelectSingleNode("gml:Surface", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.MultiSurface:
                    geometry = getMultiSurface(node.SelectSingleNode("gml:MultiSurface", xnsm), xnsm, coordinateSystem);
                    break;
                    // Mapping a gml:Curve to a kml:LineString
                case EGEOMETRYTYPE.Curve:
                    geometry = getCurve(node.SelectSingleNode("//gml:LineString", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.MultiCurve:
                    geometry = getMultiCurve(node.SelectSingleNode("gml:MultiCurve", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.MultiGeometry:
                    geometry = getMultiGeometry(node, xnsm, coordinateSystem); // to do!!
                    break;
                case EGEOMETRYTYPE.LineString:
                    geometry = getLineString(node.SelectSingleNode("gml:LineString", xnsm), xnsm, coordinateSystem);
                    break;
                case EGEOMETRYTYPE.LinearRing:
                    geometry = getLinearRing(node.SelectSingleNode("gml:LinearRing", xnsm), xnsm, coordinateSystem);
                    break;
                    #region NOTIMPLEMENTED
                case EGEOMETRYTYPE.Grid:
                case EGEOMETRYTYPE.MultiSolid:
                case EGEOMETRYTYPE.RectifiedGrid:
                    break;
                    #endregion
                default:
                    break;
            }
            return geometry;
        }

        // useful for xml multiobjects
        static string[] XMLMEMBER = { "Member", "Members" };

        // Defining single geometry primitive structure (from gml structure to kml structure)
        // useful link to know how to parse the gml object:
        // http://schemas.diggsml.com/1.1.0/SchemaDocumentation/Complete/gml3.2Profile_diggs.xsd.html
        // For multiObjects must check objectMember and objectMembers (both can contain data)
        public static Geometry getMultiPoint(XmlNode multiPointRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            //multiPointRootNode = gml:MultiPoint
            MultipleGeometry geometry = new MultipleGeometry(); 
            int size = 0;

            for (int i = 0; i < XMLMEMBER.Count(); i++) {
                XmlNodeList pointsList = multiPointRootNode.SelectNodes("gml:point" + XMLMEMBER[i] + "/gml:Point", xnsm);
                size += pointsList.Count;
                foreach (XmlNode pointNode in pointsList) geometry.AddGeometry((Point)getPoint(pointNode, xnsm, coordinateSystem));
            }
            return geometry;
        }

        public static Geometry getPoint(XmlNode pointRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // pointRootNode = gml:Point
            Point point = new Point();
            EDIMENSION dimension;
            // find cardinality 
            int cardinality = Convert.ToInt32(pointRootNode.SelectSingleNode("gml:pos", xnsm).Attributes["srsDimension"] != null ? pointRootNode.SelectSingleNode("gml:pos", xnsm).Attributes["srsDimension"].InnerText : "2");             
            switch (cardinality) {
                case 3:
                    dimension = EDIMENSION.THREE;
                    break;
                default:
                    dimension = EDIMENSION.TWO;
                    break;
            }
            point.Coordinate = PointCoordinates(pointRootNode.SelectSingleNode("gml:pos", xnsm).InnerText.Trim(), dimension, coordinateSystem);
            return point;
        }

        public static Geometry getMultiCurve(XmlNode multiCurveRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            //multiCurveRootNode = gml:MultiCurve
            MultipleGeometry geometry = new MultipleGeometry(); 
            int size = 0;
            for (int i = 0; i < XMLMEMBER.Count(); i++) {
                XmlNodeList curvesList = multiCurveRootNode.SelectNodes("gml:curve" + XMLMEMBER[i] + "/gml:LineString", xnsm);
                size += curvesList.Count;
                foreach (XmlNode curveNode in curvesList) geometry.AddGeometry((LineString)getPoint(curveNode, xnsm, coordinateSystem));
            }
            return geometry;
        }

        public static Geometry getCurve(XmlNode curveRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // curveRootNode = gml:LineString
            LineString lineString = new LineString();
            EDIMENSION dimension;
            // find cardinality 
            int cardinality = Convert.ToInt32(curveRootNode.SelectSingleNode("//gml:posList", xnsm).Attributes["srsDimension"] != null ? curveRootNode.SelectSingleNode("//gml:posList", xnsm).Attributes["srsDimension"].InnerText : "2");             
            switch (cardinality) {
                case 3:
                    dimension = EDIMENSION.THREE;
                    break;
                default:
                    dimension = EDIMENSION.TWO;
                    break;
            }
            lineString.Coordinates = Coordinates(curveRootNode.SelectSingleNode("//gml:posList", xnsm).InnerText.Trim(), dimension, coordinateSystem);  
            return lineString;
        }

        public static Geometry getMultiSurface(XmlNode multiSurfaceRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            //multiSurfaceRootNode = gml:MultiSurface
            MultipleGeometry geometry = new MultipleGeometry(); 
            int size = 0;
            for (int i = 0; i < XMLMEMBER.Count(); i++) {
                XmlNodeList surfaceList = multiSurfaceRootNode.SelectNodes("gml:surface" + XMLMEMBER[i] + "/gml:Polygon", xnsm);
                size += surfaceList.Count;
                foreach (XmlNode surfaceNode in surfaceList) geometry.AddGeometry((Polygon)getSurface(surfaceNode, xnsm, coordinateSystem));
            }
            return geometry;
        }

        public static Geometry getSurface(XmlNode surfaceRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // EGEOMETRYTYPE.Surface surfaceRootNode = gml:Surface 
            // EGEOMETRYTYPE.Polygon surfaceRootNode = gml:Polygon 
            Polygon polygon = new Polygon();                        
            OuterBoundary ospBoundary = new OuterBoundary();
            LinearRing lspRing = new LinearRing();
            EDIMENSION dimension;

            // find cardinality         
            int cardinality = Convert.ToInt32(surfaceRootNode.SelectSingleNode("gml:exterior/gml:LinearRing/gml:posList", xnsm).Attributes["srsDimension"] != null ? surfaceRootNode.SelectSingleNode("gml:exterior/gml:LinearRing/gml:posList", xnsm).Attributes["srsDimension"].InnerText : "2");             
            switch (cardinality) {
                case 3:
                    dimension = EDIMENSION.THREE;
                    break;
                default:
                    dimension = EDIMENSION.TWO;
                    break;
            }

            lspRing.Coordinates = Coordinates(surfaceRootNode.SelectSingleNode("gml:exterior/gml:LinearRing/gml:posList", xnsm).InnerText.Trim(), dimension, coordinateSystem);
            ospBoundary.LinearRing = lspRing;

            // build the polygon structure
            polygon.Extrude = true;
            polygon.AltitudeMode = AltitudeMode.ClampToGround;  // Ignore an altitude specification
            polygon.Tessellate = true;
            polygon.OuterBoundary = ospBoundary;

            return polygon;
        }
        // to do
        public static Geometry  getLinearRing(XmlNode linearRingRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // surfaceRootNode = gml:LineString
            LinearRing lr = new LinearRing();
            EDIMENSION dimension;

            // find cardinality         
            int cardinality = Convert.ToInt32(linearRingRootNode.SelectSingleNode("gml:posList", xnsm).Attributes["srsDimension"] != null ? linearRingRootNode.SelectSingleNode("gml:posList", xnsm).Attributes["srsDimension"].InnerText : "2");               
            switch (cardinality) {
                case 3:
                    dimension = EDIMENSION.THREE;
                    break;
                default:
                    dimension = EDIMENSION.TWO;
                    break;
            }

            lr.Coordinates = Coordinates(linearRingRootNode.SelectSingleNode("gml:posList", xnsm).InnerText.Trim(), dimension, coordinateSystem);

            return lr;
        }

        public static Geometry getLineString(XmlNode lineStringRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // surfaceRootNode = gml:LineString
            LineString ls = new LineString();
            EDIMENSION dimension;

            // find cardinality         
            int cardinality = Convert.ToInt32(lineStringRootNode.SelectSingleNode("gml:posList", xnsm).Attributes["srsDimension"] != null ? lineStringRootNode.SelectSingleNode("gml:posList", xnsm).Attributes["srsDimension"].InnerText : "2");               
            switch (cardinality) {
                case 3:
                    dimension = EDIMENSION.THREE;
                    break;
                default:
                    dimension = EDIMENSION.TWO;
                    break;
            }

            ls.Coordinates = Coordinates(lineStringRootNode.SelectSingleNode("gml:posList", xnsm).InnerText.Trim(), dimension, coordinateSystem);

            return ls;
        }

        public static Geometry getMultiGeometry(XmlNode geometryRootNode, XmlNamespaceManager xnsm, COORDINATE coordinateSystem) {
            // geometryRootNode = gml:MultiGeometry
            MultipleGeometry geometry = new MultipleGeometry();
            EGEOMETRYTYPE geoType = EGEOMETRYTYPE.NONE; 

            // get the list of AbstractGeometry
            XmlNodeList geometryList = geometryRootNode.SelectNodes("gml:geometryMember", xnsm);
            foreach (XmlNode memberNode in geometryList) {
                // check the geometry type
                foreach (string item in Enum.GetNames(typeof(EGEOMETRYTYPE))) {
                    if (geometryRootNode.SelectSingleNode("gml:geometryMember/gml:" + item, xnsm) != null) {
                        geoType = (EGEOMETRYTYPE)Enum.Parse(typeof(EGEOMETRYTYPE), item);
                        break;
                    }
                }

                switch (geoType) {
                    case EGEOMETRYTYPE.Curve:
                        geometry.AddGeometry(getCurve(memberNode.SelectSingleNode("gml:Point", xnsm), xnsm, coordinateSystem));
                        break;
                    case EGEOMETRYTYPE.Point:
                        geometry.AddGeometry(getPoint(memberNode.SelectSingleNode("gml:LineString", xnsm), xnsm, coordinateSystem));
                        break;
                    case EGEOMETRYTYPE.Surface:
                        geometry.AddGeometry(getSurface(memberNode.SelectSingleNode("gml:Surface", xnsm), xnsm, coordinateSystem));
                        break;
                    case EGEOMETRYTYPE.LineString:
                        geometry.AddGeometry(getLineString(memberNode.SelectSingleNode("gml:LineString", xnsm), xnsm, coordinateSystem));
                        break;
                    default:
                        // not a valid geometry for the MultyGeometry option
                        break;
                }
            }
            return geometry;
        }

    }
}

