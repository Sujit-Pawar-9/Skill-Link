using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BOL;
using BLL;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Iana;
using DAL;
namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]

    public class BookingList : Controller{

        [HttpGet("user/bookingList")]
        public IActionResult GetAllBookingList(int userID)
        {
            Console.WriteLine("Getting booking list");
            if (userID <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            // Call the BLL method to get booking lists for the specified user ID
            List<BOL.BookingList> bookingLists = BookingListManager.GetBookingListByUserId(userID);

            if (bookingLists == null)
            {
                return NotFound("No booking lists found for the specified user ID");
            }
            foreach (var item in bookingLists)
            {
                Console.WriteLine(item);
            }
            
            return Ok(bookingLists);
        }

        // [HttpPost("serviceProvider/bookingList")]
        // public IActionResult AddIntoBookingList([FromBody] BookingListViewModel model){

        //     for (int i = 0; i < model.UserIDs.Length; i++)
        //     {
        //         BookingListManager.AddIntoBookingList(modeluserId, model.serviceProviderData);
        //     }
        //     foreach (int userId in model.UserIDs){
        //         Console.WriteLine("UserID's  in array "+ userId);
                
                    
        //     }
            
        //     return Ok("Confirmed!!");
        // }
        [HttpPost("serviceProvider/bookingList")]
        public IActionResult AddIntoBookingList([FromBody] BookingListViewModel model)
        {
            // Assuming both arrays have the same length
            int length = Math.Min(model.UserIDs.Length, model.RequirementIDs.Length);
            Console.WriteLine("in controller");
            for (int i = 0; i < length; i++)
            {
                // Access elements from both arrays at the same index
                int userId = model.UserIDs[i];
                int serviceId = model.RequirementIDs[i];

                // Call the method to add booking for each user with corresponding service
                BookingListManager.AddIntoBookingList(serviceId,userId,model.serviceProviderData);
                
                // Output both UserIDs and selectedServices to console
                Console.WriteLine($"UserID: {userId}, ServiceID: {serviceId}");
            }
    
    return Ok("Confirmed!!");
}




        [HttpPost("user/bookServiceProvider")]
        public IActionResult SelectServiceProvider([FromBody] SelectServiceProviderRequest request)
        {
            Console.WriteLine("You were booked: userid ==>" + request.UserID+" username==> "+ request.ServiceProviderUsername+" status==> " +request.IsSelected);
            // Assuming you have a service to handle the selection logic
            bool selectionSuccess = BookingListManager.SelectServiceProvider(request.UserID, request.ServiceProviderUsername,request.IsSelected);

            if (selectionSuccess)
            {
                return Ok(new { message = "Service provider selected successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to select service provider" });
            }
        }

    
    }