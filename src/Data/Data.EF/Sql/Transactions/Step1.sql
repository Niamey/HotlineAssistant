CREATE OR ALTER VIEW [SOLAR_BO].[vw_bo_original_txn_data]
AS
SELECT
	TxnId = [ID],
	AcqInstitutionCode = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.acqInstitutionCode'), null),
	TerminalType = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.posData.terminalType'), null),
	CardDataInputMode = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.posData.cardDataInputMode'), null),
	OnlinePinVerificationResult = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.cardAuthData.onlinePinVerificationResult'), null),
	Cvv2VerificationResult = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.cardAuthData.cvv2VerificationResult'), null),
	CavvValidationResult = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.cardAuthData.cavvValidationResult'), null),
	Stan = IIF(ISJSON(SERIALIZED_MODEL) = 1, JSON_VALUE(SERIALIZED_MODEL, '$.cardAuthData.stan'), null)
FROM [SOLAR_BO].[BO_ORIGINAL_TXN_DATA]