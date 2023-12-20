using FlightAgreementApplication.DTO;
using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.FAAData;
using FlightAgreementApplication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FlightAgreementApplication.FAAWrapper
{
    public class FAAAirlineManagerOperationsWrapper
    {

        private readonly FAAAirlineManagerOperationsData _data;

        public FAAAirlineManagerOperationsWrapper(FAAAirlineManagerOperationsData data)
        {
            _data = data;
        }

        public ActionResult<IEnumerable<TourOperator>> GetAllTourOperators() {

            try
            {
                var tourOperators = _data.GetAllTourOperators();
                return tourOperators;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public ActionResult<TourOperator> GetTourOperatorById(Guid tourOperatorId)
        {
            try
            {
                var tourOperator = _data.GetTourOperatorById(tourOperatorId);
                return tourOperator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> AddTourOperator(TourOperatorDto newTourOperatorDto)
        {
            try
            {
                var response = await _data.AddTourOperator(newTourOperatorDto);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string UpdateTourOperator(Guid tourOperatorId, updateTourOperator updatedTourOperatorDto)
        {

            var msg = _data.UpdateTourOperator(tourOperatorId, updatedTourOperatorDto);
            return msg;

        }


        public String DeleteTourOperator(Guid tourOperatorId)
        {
            var msg = _data.DeleteTourOperator(tourOperatorId);
            return msg;
        }
    }
}
