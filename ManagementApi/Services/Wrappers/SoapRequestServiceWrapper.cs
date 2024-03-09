﻿namespace ManagementApi.Services.Wrappers
{
    using ManagementApi.Factories;
    using ManagementApi.Models;
    using ManagementApi.Services.Interfaces;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class SoapRequestServiceWrapper : ISoapRequestServiceWrapper
    {
        private readonly SqlConnection _sqlConnection;

        public SoapRequestServiceWrapper(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<string> CreateSOAPRequest(SOAPRequestServiceRequest request)
        {
            var wrapper = GetSqlConnectionWrapper();
            var soapFactory = GetSoapFactory(wrapper);
            var soapRequestService = soapFactory.Create(request);
            var result = await soapRequestService.CreateSOAPRequest(request.Input);
            return result;
        }

        private SqlConnectionWrapper GetSqlConnectionWrapper() => new(_sqlConnection);

        private static SOAPRequestServiceFactory GetSoapFactory(ISqlConnectionWrapper wrapper) => new(wrapper);
    }
}
