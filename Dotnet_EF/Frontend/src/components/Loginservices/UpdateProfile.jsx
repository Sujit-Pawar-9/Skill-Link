import axios from "axios";
import { useState } from "react";
import { Form, useNavigate } from "react-router-dom";
// import "../LoginForm.css";
import "./UpdateProfile.css";
import { FaEye, FaEyeSlash } from "react-icons/fa6";
const UpdateProfile=()=>{
    const userId = sessionStorage["userId"];
    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();
   
    const userData = JSON.parse(localStorage.getItem("user"));
    const [formData, setFormData] = useState({
        NameFirst: userData.nameFirst,
        NameLast: userData.nameLast,
        Username: userData.username,
        Password: userData.password,
        PhoneNumber: userData.phoneNumber,
        Address: userData.address
      });
     
    const handleChange = (e) => {
      const { name, value } = e.target;
      setFormData((prevState) => ({
        ...prevState,
        [name]: value
      }));
    };
  
    const handleSubmit = async (e) => {
      e.preventDefault();
  
      try {
        console.log(formData);
        const response = await axios.post('http://localhost:5020/api/User/update', formData).then(() => {
          navigate("/servicelist");
        });
      } catch (error) {
        console.error('Error:', error);
      }
    };



    return (
        <center className="login-center1">
        <h1>Edit Your Profile</h1>
        <h3>
          <b> {formData.Username}</b>
        </h3>
        <div className="container1">
          <h3></h3>
          <form onSubmit={handleSubmit}>
          <input
                type="text"
                placeholder="First Name"
                name="NameFirst"
                value={formData.NameFirst}
                onChange={handleChange} 
                title="Please enter only characters"
                required
                pattern="[A-Za-z]+"
              />
              <input
                type="text"
                placeholder="Last Name"
                name="NameLast"
                value={formData.NameLast}
                onChange={handleChange}
                title="Please enter only characters"
                required
                pattern="[A-Za-z]+"
              />
              {/* <input
                type="text"
                placeholder="Username"
                name="Username"
                value={formData.Username}
                onChange={handleChange} required
              /> */}
              <div className="pass-input-div">
                <input
                  type={showPassword ? "text" : "password"}
                  placeholder="Password (8-15 characters, 1 uppercase, 1 digit, 1 special character)"
                  name="Password"
                  value={formData.Password}
                  onChange={handleChange} required
                  pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,15}$"
                  />
                
                {showPassword ? (
                  <FaEyeSlash onClick={() => setShowPassword(!showPassword)} />
                ) : (
                  <FaEye onClick={() => setShowPassword(!showPassword)} />
                )}
              </div>
              <input
                type="text"
                placeholder="Phone Number"
                name="PhoneNumber"
                maxLength={10}
                value={formData.PhoneNumber}
                pattern="[6-9]{1}[0-9]{9}"
                onChange={handleChange} required
              />
              <input
                type="text"
                placeholder="Address"
                name="Address"
                value={formData.Address}
                onChange={handleChange}
                pattern="[A-Za-z]+"
                title="Please enter only characters"
                required
              />
            <div className="login-center1-options"></div>
            <div className="login-center1-buttons">
              <button type="submit">Update Profile</button>
            </div>
          </form>
        </div>
      </center>
      );
}
export default UpdateProfile;











// import axios from "axios";
// import { useState, useEffect } from "react";
// import { useNavigate } from "react-router-dom";
// import { FaEye, FaEyeSlash } from "react-icons/fa6";
// import "./UpdateProfile.css";

// const UpdateProfile = () => {
//   const userId = sessionStorage["userId"];
//   const [showPassword, setShowPassword] = useState(false);
//   const navigate = useNavigate();

//   const [formData, setFormData] = useState({
//     NameFirst: "",
//     NameLast: "",
//     Username: "",
//     Password: "",
//     PhoneNumber: "",
//     Address: ""
//   });

//   useEffect(() => {
//     // Fetch user data from the server or use your preferred method to get the user's data
//     const fetchUserData = async () => {
//       try {
//         const response = await axios.get(`http://localhost:7373/user/${userId}`);
//         const userData = response.data; // Replace with the actual endpoint for fetching user data
        
//       } catch (error) {
//         console.error("Error fetching user data:", error);
//       }
//     };

//     // Call the fetchUserData function when the component mounts
//     fetchUserData();
//   }, [userId]);

//   const handleChange = (e) => {
//     const { name, value } = e.target;
//     setFormData((prevState) => ({
//       ...prevState,
//       [name]: value
//     }));
//   };

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     // Implement the logic for updating the user's profile
//     try {
//       const response = await axios.put(`http://localhost:7373/user/${userId}`, formData);
//       // Handle the response accordingly
//     } catch (error) {
//       console.error("Error updating user data:", error);
//     }
//   };

//   return (
   
//   );
// };

// export default UpdateProfile;
