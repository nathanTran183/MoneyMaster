using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Responses;

namespace MoneyMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        // GET api/SubCategories
        [HttpGet]
        public ActionResult<ResponseResult<IEnumerable<SubCategoryDTO>>> GetSubCategories()
        {
            throw new NotImplementedException();
        }
    }
}
