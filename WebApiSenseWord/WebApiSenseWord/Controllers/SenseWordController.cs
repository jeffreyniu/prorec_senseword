using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Adapter.SenseWord.Criteria;
using Application.Adapter.SenseWord.Interfaces;
using CustomCore;
using CustomException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ThirdParties.Authentication.Interfaces;
using WebApiSenseWord.Controllers.Contracts.SenseWord;
using WebApiSenseWord.Mappers.Interfaces;
using WebApiSenseWord.Models;

namespace WebApiSenseWord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenseWordController : BaseApiController
    {
        private readonly ISenseWordAdapter _senseWordAdapter;
        private readonly ISenseWordMapper _senseWordMapper;
        private readonly ITokenAuth _tokenAuth;

        #region Constructor

        public SenseWordController(IOptions<AppSettings> appSettings,
            ISenseWordAdapter senseWordAdapter, 
            ISenseWordMapper senseWordMapper, 
            ITokenAuth tokenAuth):base(appSettings)
        {
            _senseWordAdapter = senseWordAdapter;
            _senseWordMapper = senseWordMapper;

            _tokenAuth = tokenAuth;
            _tokenAuth.BaseAddress = appSettings.Value.BaseAddressOfGo;
            _tokenAuth.AuthenticationUrl = AppSettings.Value.AuthenticationUrl;
        }

        #endregion

        #region Action

        [HttpGet]
        public ActionResponseModel Get([FromQuery] GetRequest request)
        {
            var actionResponseModel = new ActionResponseModel();

            try
            {
                if (request.UserId <= 0 || string.IsNullOrEmpty(request.TableName) || request.WordId <= 0)
                {
                    throw new BadRequestException("The value of parameter(s) are invalid");
                }

                if (_tokenAuth.IsAuthenticated(GetRequestHeaderContract()))
                {
                    var entity = _senseWordAdapter.Get(new GetCriterion
                    {
                        UserId = request.UserId,
                        TableName = request.TableName,
                        WordId = request.WordId
                    });
                    if (entity == null)
                    {
                        throw new NotFoundException($"Not found the SenseWordEntity for request {JsonSerializer.Serialize(request)}");
                    }

                    actionResponseModel.Status = System.Net.HttpStatusCode.OK;
                    actionResponseModel.Data = JsonSerializer.Serialize(_senseWordMapper.Map(entity));
                    actionResponseModel.Error = string.Empty;
                }
            }
            catch(BadRequestException ex)
            {
                actionResponseModel.Status = System.Net.HttpStatusCode.BadRequest;
                actionResponseModel.Error = ex.Message;
            }
            catch(UnauthorizedAccessException ex)
            {
                actionResponseModel.Status = System.Net.HttpStatusCode.Unauthorized;
                actionResponseModel.Error = ex.Message;
            }
            catch(NotFoundException ex)
            {
                actionResponseModel.Status = System.Net.HttpStatusCode.NotFound;
                actionResponseModel.Error = ex.Message;
            }
            catch(Exception ex)
            {
                actionResponseModel.Status = System.Net.HttpStatusCode.InternalServerError;
                actionResponseModel.Error = ex.Message;
            }
            return actionResponseModel;
        }

        #endregion
    }
}
