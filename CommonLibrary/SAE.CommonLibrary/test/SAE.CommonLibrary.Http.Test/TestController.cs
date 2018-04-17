using SAE.CommonLibrary.Http;
using SAE.Test.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SAE.CommonLibrary.HttpTest
{
    [Route("/api/[action]")]
    public class TestController : Controller
    {
        public TestController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Upload(Student student)
        {
            var form = await this.Request.ReadFormAsync();
            if (this.Request.Form.Files.Count > 0)
            {

                return Json(new { query = this.Request.Form.Files[0].FileName, student = student, form = form });
            }
            else
            {
                return this.StatusCode(502);
            }
        }

        [HttpGet]
        public IActionResult Value([FromBody]Student student)
        {
            if (this.Request.Query.Count > 0)
            {
                return Json(new { query = this.Request.QueryString.Value, student = student });
            }
            else
            {
                return this.StatusCode(502);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]Student student)
        {

            if (this.Request.Query.Count > 0)
            {
                return Json(new { query = this.Request.QueryString.Value, student = student });
            }
            else
            {
                return this.StatusCode(502);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]Student student)
        {
            if (this.Request.Query.Count > 0)
            {
                return Json(new { query = this.Request.QueryString.Value, student = student });
            }
            else
            {
                return this.StatusCode(502);
            }
        }

        [HttpOptions]
        public async Task<IActionResult> Create([FromBody]Student student)
        {
            if (this.Request.Query.Count > 0)
            {
                return Json(new { query = this.Request.QueryString.Value, student = student });
            }
            else
            {
                return this.StatusCode(502);
            }
        }

    }
}
