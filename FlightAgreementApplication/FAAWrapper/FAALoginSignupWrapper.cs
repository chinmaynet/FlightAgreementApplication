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

        public AuthResponse Login(LoginRequestDto request)
        {
            try
            {
                AuthResponse userdetails = _data.Login(request);
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

        public async Task<ResetPasswordResponse> ResetPasswordRequest( ResetPasswordRequestDto request) {

            try {
                var resetPasswordResponse = await _data.ResetPasswordRequest(request);

                return resetPasswordResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResetPasswordVerifyResponse> ResetPasswordVerify( ResetPasswordVerifyRequestDto request)
        {
            try {
                var resetPasswordVerifyResponse = await _data.ResetPasswordVerify(request);
                return resetPasswordVerifyResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UpdateUserDetailsResponse> UpdateUserDetails( Guid userId,  UpdateUserDetailsDto userDetailsDto)
        {

            try
            {
                var updateUserDetailsResponse = await _data.UpdateUserDetails(userId, userDetailsDto);
                return updateUserDetailsResponse;
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
