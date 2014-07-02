namespace Terradue.OpenSearch.Kml.Result
{
	using System;
	using System.Collections.Generic;
	
	using System.Runtime.Serialization;


	[DataContract(Name="sourceProducts")]
	public class SourceProducts
	{
		public SourceProducts() {}

		public List<EarthObservation> EarthObservations { get; set; }
	}
	
	/// <summary>
	///   Defines the EarthObservation type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class EarthObservation
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="EarthObservation"/> class.
		/// </summary>
		public EarthObservation() {}

		[DataMember(Name="gml_beginPosition")]
		public string BeginPosition { get; set; }

		[DataMember(Name="gml_endPosition")]
		public string EndPosition { get; set; }

		[DataMember(Name="gml_timePosition")]
		public string TimePosition { get; set; }

		[DataMember(Name="EarthObservationEquipment")]
		public EarthObservationEquipment EarthObservationEquipment { get; set; }

		[DataMember(Name="om_observedProperty")]
		public string ObservedProperty { get; set; }

		[DataMember(Name="EarthObservationResult")]
		public EarthObservationResult EarthObservationResult { get; set; }
		
		[DataMember(Name="EarthObservationMetaData")]
		public EarthObservationMetaData EarthObservationMetaData { get; set; }


	}

	/// <summary>
	///   Defines the EarthObservation type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class EarthObservationEquipment
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="EarthObservationEquipment"/> class.
		/// </summary>
		public EarthObservationEquipment() {}

		[DataMember(Name="eop_platformShortName")]
		public string PlatformShortName { get; set; }
		
		[DataMember(Name="eop_platformSerialIdentifier")]
		public string PlatformSerialIdentifier { get; set; }
		
		[DataMember(Name="eop_instrumentShortName")]
		public string InstrumentShortName { get; set; }
		
		[DataMember(Name="eop_sensorType")]
		public string SensorType { get; set; }

		[DataMember(Name="eop_operationalMode")]
		public string OperationalMode { get; set; }

		[DataMember(Name="eop_swathIdentifier")]
		public string SwathIdentifier { get; set; }

		[DataMember(Name="Acquisition")]
		public Acquisition Acquisition { get; set; }
		
	}

	/// <summary>
	///   Defines the Acquisition type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class Acquisition
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Acquisition"/> class.
		/// </summary>
		public Acquisition() {}
		
		[DataMember(Name="eop_orbitNumber")]
		public uint OrbitNumber { get; set; }
		
		[DataMember(Name="eop_orbitDirection")]
		public string OrbitDirection { get; set; }
		
		[DataMember(Name="eop_wrsLongitudeGrid")]
		public string WrsLongitudeGrid { get; set; }
		
		[DataMember(Name="eop_wrsLatitudeGrid")]
		public string WrsLatitudeGrid { get; set; }

		[DataMember(Name="eop_startTimeFromAscendingNode")]
		public int StartTimeFromAscendingNode { get; set; }
		
		[DataMember(Name="eop_completionTimeFromAscendingNode")]
		public int CompletionTimeFromAscendingNode { get; set; }
		
		[DataMember(Name="eop_illuminationAzimuthAngle")]
		public string IlluminationAzimuthAngle { get; set; }

		[DataMember(Name="eop_illuminationZenithAngle")]
		public string IlluminationZenithAngle { get; set; }

		[DataMember(Name="eop_illuminationElevationAngle")]
		public string IlluminationElevationAngle { get; set; }

		[DataMember(Name="atm_multiViewAngles")]
		public string MultiViewAngles { get; set; }

		[DataMember(Name="atm_centreViewAngles")]
		public string CentreViewAngles { get; set; }

		[DataMember(Name="sar_polarisationMode")]
		public string PolarisationMode { get; set; }

		[DataMember(Name="sar_polarisationChannels")]
		public string PolarisationChannels { get; set; }

		[DataMember(Name="sar_antennaLookDirection")]
		public string AntennaLookDirection { get; set; }

		[DataMember(Name="sar_minimumIncidenceAngle")]
		public string MinimumIncidenceAngle { get; set; }

		[DataMember(Name="sar_maximumIncidenceAngle")]
		public string MaximumIncidenceAngle { get; set; }

		[DataMember(Name="sar_incidenceAngleVariation")]
		public string IncidenceAngleVariation { get; set; }

		[DataMember(Name="sar_dopplerFrequency")]
		public string DopplerFrequency { get; set; }
		
	}

	/// <summary>
	///   Defines the EarthObservationResult type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class EarthObservationResult
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="EarthObservationResult"/> class.
		/// </summary>
		public EarthObservationResult() {}
		
		[DataMember(Name="eop_BrowseInformation")]
		public BrowseInformation BrowseInformation { get; set; }
		
		[DataMember(Name="eop_ProductInformation")]
		public ProductInformation ProductInformation { get; set; }
		
		[DataMember(Name="atm_cloudCoverPercentage")]
		public double CloudCoverPercentage { get; set; }
		
		[DataMember(Name="atm_snowCoverPercentage")]
		public uint SnowCoverPercentage { get; set; }
		
		[DataMember(Name="opt_cloudCoverPercentage")]
		public uint CloudCoverPercentage1 { get; set; }
		
		[DataMember(Name="opt_snowCoverPercentage")]
		public uint SnowCoverPercentage1 { get; set; }
		
		
	}

	/// <summary>
	///   Defines the BrowseInformation type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class BrowseInformation
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BrowseInformation"/> class.
		/// </summary>
		public BrowseInformation() {}
		
		[DataMember(Name="eop_layer")]
		public string Title { get; set; }

		[DataMember(Name="eop_type")]
		public string Type { get; set; }

		[DataMember(Name="eop_subtype")]
		public string SubType { get; set; }
		
		[DataMember(Name="eop_referenceSystemIdentifier")]
		public string ReferenceSystemIdentifier { get; set; }
		
		[DataMember(Name="eop_url")]
		public string Url { get; set; }
		
	}

	/// <summary>
	///   Defines the ProductInformation type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class ProductInformation
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductInformation"/> class.
		/// </summary>
		public ProductInformation() {}
		
		[DataMember(Name="eop_filename")]
		public string Filename { get; set; }
		
		[DataMember(Name="eop_version")]
		public string Version { get; set; }
		
		[DataMember(Name="eop_size")]
		public int Size { get; set; }
		
		[DataMember(Name="EarthObservationResult")]
		public EarthObservationResult EarthObservationResult { get; set; }
		
		
	}

	/// <summary>
	///   Defines the EarthObservationMetaData type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class EarthObservationMetaData
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="EarthObservationMetaData"/> class.
		/// </summary>
		public EarthObservationMetaData() {}
		
		[DataMember(Name="eop_identifier")]
		public string Identifier { get; set; }
		
		[DataMember(Name="eop_parentIdentifier")]
		public string ParentIdentifier { get; set; }

		[DataMember(Name="eop_acquisitionType")]
		public string AcquisitionType { get; set; }	

		[DataMember(Name="eop_acquisitionSubType")]
		public string AcquisitionSubType { get; set; }

		[DataMember(Name="eop_productType")]
		public string ProductType { get; set; }
		
		[DataMember(Name="eop_status")]
		public string Status { get; set; }
		
		[DataMember(Name="eop_imageQualityDegradation")]
		public string ImageQualityDegradation { get; set; }

		[DataMember(Name="eop_imageQualityStatus")]
		public string ImageQualityStatus { get; set; }

		[DataMember(Name="eop_imageQualityDegradationTag")]
		public string ImageQualityDegradationTag { get; set; }

		[DataMember(Name="eop_imageQualityReportURL")]
		public string ImageQualityReportURL { get; set; }

		[DataMember(Name="eop_processing")]
		public Processing Processing { get; set; }

		[DataMember(Name="eop_productGroupId")]
		public string ProductGroupId { get; set; }

		[DataMember(Name="eop_localAttribute")]
		public List<string> LocalAttribute { get; set; }

		[DataMember(Name="eop_localValue")]
		public List<string> LocalValue { get; set; }

	}

	/// <summary>
	///   Defines the Processing type as in ngEO-14-ICD-TPZ-076, section 7
	/// </summary>
	[DataContract]
	public class Processing
	{
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Processing"/> class.
		/// </summary>
		public Processing() {}
		
		[DataMember(Name="eop_processingMode")]
		public string ProcessingMode { get; set; }
	}
}