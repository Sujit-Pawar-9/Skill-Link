using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BOL;
using BLL;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Iana;
namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]

public class ServiceProvider : Controller{
    private readonly ILogger<ServiceProvider> _logger;

    public ServiceProvider(ILogger<ServiceProvider> logger){
        _logger = logger;
    }

     
    [HttpPost("login")] 
    public IActionResult serviceproviderlogin([FromForm] string Username,[FromForm] string Password){
        Console.WriteLine("service provider login");
        BOL.ServiceProvider serviceProviderExists = ServiceProviderManager.ValidateServiceProvider(Username, Password);
        if (serviceProviderExists!=null){
            HttpContext.Session.SetInt32("ServiceProviderID", serviceProviderExists.ServiceProviderID);
            return Ok(new { message = "Login successful" ,serviceProviderExists=serviceProviderExists});
        }
        else{
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }

    [HttpPost("registration")]
    public IActionResult RegisterServiceProvider([FromBody] BOL.ServiceProvider serviceProvider){
        if (serviceProvider == null){
            return BadRequest("Invalid request body");
        }

         bool registrationSuccess = ServiceProviderManager.RegisterServiceProvider(serviceProvider);

        if (registrationSuccess){
            return Ok(new { message = "ServiceProvider registered successfully!" });
        }
        else{
            return BadRequest(new { message = "Failed to register service provider. Username already exists." });
        }
    }

    [HttpGet("userrequirements")]
    public IActionResult GetUserRequirements(string skills){
        Console.WriteLine("in get user requirement");
            try
            {
                List<UserRequirementWithUserData> userRequirements = ServiceProviderManager.GetUserRequirements(skills);
                
                return Ok(userRequirements);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving user requirements: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to retrieve user requirements" });
            }
        }
        [HttpGet("getStatus")]
        public IActionResult GetServiceProviderData(string serviceProviderUsername)
        {
            List<ServiceProviderViewStatus> serviceProviderData = ServiceProviderManager.GetServiceProviderData(serviceProviderUsername);
            if (serviceProviderData != null && serviceProviderData.Count > 0)
            {
                return Ok(serviceProviderData);
            }
            else
            {
                return NotFound();
            }
        }
    

        // [HttpGet("getFeedback")]
        // public IActionResult GetFeedbacksByServiceProvider(string serviceProviderUsername)
        // {
        //     List<Feedback> feedbacks = ServiceProviderManager.GetFeedbacksByServiceProvider(serviceProviderUsername);
        //     return Ok(feedbacks);
        // }


        
[HttpPost("ServiceUpdate")]
public IActionResult ServiceUpdate([FromBody] BOL.ServiceProvider serviceData){
    Console.WriteLine("Inside Update");
    bool updateStatus=ServiceProviderManager.UpdateService(serviceData);
    if(updateStatus){
        return Ok(new{ message="serviceProvider updated"});
    }
    else{
         return BadRequest(new { message = "Failed to failed ServicePRovider" });
    }
}
     





[HttpPost("ServiceforgotPassword")]  // Consider a more meaningful route
public IActionResult ForgotPassword([FromForm] string Username, [FromForm] string Password)
{
    Console.WriteLine("service Forgot " + Username + Password);

    // Assuming UserManager.ChangePassword is your method for handling password change
    bool changePass = ServiceProviderManager.ChangePassword(Username, Password);

    if (changePass)
    {
        return Ok(new { message = "Password Changed" });
    }
    else
    {
        return Unauthorized(new { message = "Invalid Credentials" });
    }
}
}


    
