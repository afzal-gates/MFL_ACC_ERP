using ERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSolution.Controllers
{
    public class UserApiController : ControllerBase
    {
        // GET: api/UserApi
        //[Authorize]
        public IActionResult Get(string LOGIN_ID)
        {
            try
            {
                var ob = new UserModelApi().SelectDatabyLoginId(LOGIN_ID);
                return Ok(ob);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult get(string LOGIN_ID, string PUSH_REGI_ID)
        {
            try
            {
                var ob = new UserModelApi() {
                    LOGIN_ID = LOGIN_ID,
                    PUSH_REGI_ID = PUSH_REGI_ID
                };
                var obj = new UserModelApi().registerPush(ob);

                if (obj == null)
                {
                    return Conflict();
                }

                return CreatedAtAction(nameof(Get), new { LOGIN_ID = obj.LOGIN_ID }, obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
