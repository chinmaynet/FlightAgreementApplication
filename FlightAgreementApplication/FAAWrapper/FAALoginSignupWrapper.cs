using FlightAgreementApplication.Dto;
using FlightAgreementApplication.DTO;
using FlightAgreementApplication.DTO.Request;
using FlightAgreementApplication.DTO.Response;
using FlightAgreementApplication.FAAData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace FlightAgreementApplication.FAAWrapper
{
    public class FAALoginSignupWrapper
    {
        private readonly FAALoginSignupData _data;

        public FAALoginSignupWrapper(FAALoginSignupData data)
        {
            _data = data;
        }

        public LoginResponse Login(LoginRequestDto request)
        {
            try
            {
                LoginResponse userdetails = _data.Login(request);
                return userdetails;
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AirlineManagerCreateResponse> CreateAirlineManager(AirlineManagerRequestDto requestDto)
        {
            try 
            {


                AirlineManagerCreateResponse airlineManagerCreateResponse =await _data.CreateAirlineManager(requestDto);
                return airlineManagerCreateResponse;
            
            
            
            } catch (Exception ex) { throw ex; }
            
        
        }

        public async Task<TourOperatorCreateResponse> CreateTourOperator(TourOperatorDto tourOperatorDto)
        {
            try
            {
                TourOperatorCreateResponse tourOperatorCreateResponse = await _data.CreateTourOperator(tourOperatorDto);
                    return tourOperatorCreateResponse;
            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<ActivateAccountResponse> ActivateAccount(ActivationRequestDto request) {
        try
        {

            ActivateAccountResponse activateAccountResponse = await _data.ActivateAccount(request);
            return activateAccountResponse;
        }
        catch (Exception ex) { throw ex; }

        }


        public async Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordDto request) {

            try
            {
                var updatePasswordResponse  = await _data.UpdatePassword(request);

                return updatePasswordResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





























        private IActionResult StatusCode(int v, object value)
        {
            throw new NotImplementedException();
        }

        private IActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }

        private IActionResult Ok(IActionResult userdetails)
        {
            throw new NotImplementedException();
        }
    }
}
