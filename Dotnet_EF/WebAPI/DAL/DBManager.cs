using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BOL; // Import your entity classes namespace
using IOCWebApp.Contexts; // Import your DbContext namespace

namespace DAL
{
    public class DBManager
    {
        public static User IsUserPresent(string username, string password)
{
    try
    {
        using (var context = new CollectionContext())
        {
            // Check if a user with the provided credentials exists
            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }
    catch (Exception ex)
    {
        // Handle any exceptions that occur during the database operation
        Console.WriteLine("Error validating user login: " + ex.Message);
        return null; // Return null in case of an error
    }
}
        public static bool RegisterUser(User userData)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    // Check if username already exists
                    var existingUser = context.Users.FirstOrDefault(u => u.Username == userData.Username);
                    if (existingUser != null)
                    {
                        Console.WriteLine("User with the same username already exists.");
                        return false;
                    }

                    // If username does not exist, proceed with user registration
                    context.Users.Add(userData);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering user: " + ex.Message);
                return false;
            }
        }

        public static int InsertUserRequirement(int userId, string skills, string wages, string address, string date)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    // Create a new UserRequirement object
                    var userRequirement = new UserRequirement
                    {
                        UserID = userId,
                        Skills = skills,
                        Wages = wages,
                        Address = address,
                        Date = date
                    };

                    // Add the UserRequirement to the context and save changes
                    context.UserRequirements.Add(userRequirement);
                    context.SaveChanges();

                    Console.WriteLine("User requirement inserted successfully");

                    return userRequirement.RequirementID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting user requirement: " + ex.Message);
                return 0;
            }
        }
        public static bool RegisterServiceProvider(BOL.ServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    // Check if username already exists
                    var existingServiceProvider = context.ServiceProviders.FirstOrDefault(sp => sp.Username == serviceProvider.Username);
                    if (existingServiceProvider != null)
                    {
                        Console.WriteLine("Service provider with the same username already exists.");
                        return false;
                    }

                    // If username does not exist, proceed with service provider registration
                    context.ServiceProviders.Add(serviceProvider);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering service provider: " + ex.Message);
                return false;
            }
        }

        public static BOL.ServiceProvider ValidateServiceProvider(string username, string password)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    // Find the service provider with the provided credentials
                    var serviceProvider = context.ServiceProviders.FirstOrDefault(sp => sp.Username == username && sp.Password == password);
                    return serviceProvider;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error validating service provider login: " + ex.Message);
                return null;
            }
        }
         public static List<UserRequirementWithUserData> GetUserRequirementsWithUserInfo(string skills)
        {
            List<UserRequirementWithUserData> userRequirements = new List<UserRequirementWithUserData>();

            try
            {
                using (var context = new CollectionContext())
                {
                    // Retrieve user requirements along with user information
                    userRequirements = context.UserRequirements
                        .Include(ur => ur.User)
                        .Where(ur => ur.Skills == skills)
                        .Select(ur => new UserRequirementWithUserData
                        {
                            RequirementID = ur.RequirementID,
                            UserID = ur.User.UserID,
                            NameFirst = ur.User.NameFirst,
                            NameLast = ur.User.NameLast,
                            PhoneNumber = ur.User.PhoneNumber,
                            Skills = ur.Skills,
                            Wages = ur.Wages,
                            Address = ur.Address,
                            Date = ur.Date
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine("Error fetching user requirements with user info: " + ex.Message);
            }

            return userRequirements;
        }

        public static BOL.ServiceProvider GetServiceProviderByID(int serviceProviderID)
        {
            BOL.ServiceProvider serviceProvider = null;

            try
            {
                using (var context = new CollectionContext())
                {
                    // Retrieve service provider by ID
                    serviceProvider = context.ServiceProviders.FirstOrDefault(sp => sp.ServiceProviderID == serviceProviderID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving service provider: " + ex.Message);
                // You may want to log the exception or handle it appropriately
            }

            return serviceProvider;
        }

      public static bool AddBookingEntry(int requirementID, int userID, BOL.ServiceProvider serviceProvider)
{
    Console.WriteLine("in db");
    try
    {
        using (var context = new CollectionContext())
        {
            // Ensure that the User and ServiceProvider exist in the context
            var user = context.Users.FirstOrDefault(u => u.UserID == userID);
            if (user == null)
            {
                Console.WriteLine($"User with ID {userID} not found.");
                return false;
            }

            var serviceProviderEntity = context.ServiceProviders.FirstOrDefault(sp => sp.ServiceProviderID == serviceProvider.ServiceProviderID);
            if (serviceProviderEntity == null)
            {
                Console.WriteLine($"ServiceProvider with ID {serviceProvider.ServiceProviderID} not found.");
                return false;
            }

            // Create a new BookingList entity
            var bookingEntry = new BookingList
            {
                UserID = userID,
                User = user,
                Username = user.Username, // Assigning username from User entity
                NameFirst = serviceProvider.NameFirst,
                NameLast = serviceProvider.NameLast,
                PhoneNumber = serviceProvider.PhoneNumber,
                Skills = serviceProvider.Skills,
                Wages = serviceProvider.Wages,
                ServiceProviderID = serviceProvider.ServiceProviderID,
                ServiceProvider = serviceProviderEntity
            };

            // Add the booking entry to the context and save changes
            context.BookingLists.Add(bookingEntry);
            context.SaveChanges();

            Console.WriteLine("Added into booking list");

            return true;
        }
    }
    catch (Exception ex)
    {
        // Log or handle the exception
        Console.WriteLine("Error adding booking entry: " + ex.Message);
        return false;
    }
}


        public static List<BookingList> GetBookingListByUserId(int userId)
        {
            List<BookingList> bookingLists = new List<BookingList>();

            try
            {
                using (var context = new CollectionContext())
                {
                    // Retrieve booking lists by user ID
                    bookingLists = context.BookingLists.Where(bl => bl.UserID == userId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error fetching booking lists: " + ex.Message);
            }

            return bookingLists;
        }

        public static bool SelectServiceProvider(int userId, string serviceProviderUsername, bool isSelected)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    // Create a new Status entity
                    var status = new Status
                    {
                        RequirementID = userId, // Assuming RequirementID is used for userId
                        ServiceProviderUserName = serviceProviderUsername,
                        IsSelected = isSelected
                    };

                    // Add the status to the context and save changes
                    context.Statuses.Add(status);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error selecting service provider: " + ex.Message);
                return false;
            }
        }
         public static List<ServiceProviderViewStatus> GetServiceProviderData(string serviceProviderUsername)
        {
            List<ServiceProviderViewStatus> serviceProviderDataList = new List<ServiceProviderViewStatus>();

            try
            {
                using (var context = new CollectionContext())
                {
                    serviceProviderDataList = context.Users
                        .Where(u => u.Statuses.Any(s => s.ServiceProviderUserName == serviceProviderUsername))
                        .Select(u => new ServiceProviderViewStatus
                        {
                            NameFirst = u.NameFirst,
                            NameLast = u.NameLast,
                            PhoneNumber = u.PhoneNumber,
                            Address = u.UserRequirements.FirstOrDefault().Address,
                            Wages = u.UserRequirements.FirstOrDefault().Wages,
                            Date = u.UserRequirements.FirstOrDefault().Date
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error fetching service provider data: " + ex.Message);
            }

            return serviceProviderDataList;
        }
        // public static bool GiveFeedback(Feedback feedback)
        // {
        //     try
        //     {
        //         using (var context = new CollectionContext())
        //         {
        //             context.Feedbacks.Add(feedback);
        //             context.SaveChanges();
        //             return true;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log or handle the exception
        //         Console.WriteLine("Error giving feedback: " + ex.Message);
        //         return false;
        //     }
        // }

        // public static List<Feedback> GetFeedbacksByServiceProvider(int serviceProviderID)
        // {
        //     List<Feedback> feedbacks = new List<Feedback>();

        //     try
        //     {
        //         using (var context = new CollectionContext())
        //         {
        //             feedbacks = context.Feedbacks
        //                 .Where(f => f.ServiceProviderID == serviceProviderID)
        //                 .ToList();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         // Log or handle the exception
        //         Console.WriteLine("Error getting feedbacks: " + ex.Message);
        //     }

        //     return feedbacks;
        // }

        public static bool UpdateUser(User userData)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == userData.Username);

                    if (user != null)
                    {
                        user.NameFirst = userData.NameFirst;
                        user.NameLast = userData.NameLast;
                        user.Password = userData.Password;
                        user.PhoneNumber = userData.PhoneNumber;
                        user.Address = userData.Address;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // User not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error updating user: " + ex.Message);
                return false;
            }
        }

        public static bool ChangePassword(string Username, string Password)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == Username);

                    if (user != null)
                    {
                        user.Password = Password;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // User not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error changing password: " + ex.Message);
                return false;
            }
        }
        public static bool UpdateService(BOL.ServiceProvider ServiceuserData)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    var serviceProvider = context.ServiceProviders.FirstOrDefault(sp => sp.Username == ServiceuserData.Username);

                    if (serviceProvider != null)
                    {
                        serviceProvider.NameFirst = ServiceuserData.NameFirst;
                        serviceProvider.NameLast = ServiceuserData.NameLast;
                        serviceProvider.Password = ServiceuserData.Password;
                        serviceProvider.PhoneNumber = ServiceuserData.PhoneNumber;
                        serviceProvider.Address = ServiceuserData.Address;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // Service provider not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error updating service provider: " + ex.Message);
                return false;
            }
        }

        public static bool ServiceChangePassword(string Username, string Password)
        {
            try
            {
                using (var context = new CollectionContext())
                {
                    var serviceProvider = context.ServiceProviders.FirstOrDefault(sp => sp.Username == Username);

                    if (serviceProvider != null)
                    {
                        serviceProvider.Password = Password;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // Service provider not found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error changing service provider password: " + ex.Message);
                return false;
            }
        }
    }
}
