import axios from "axios";
import { useState } from "react";
import { Form, useNavigate } from "react-router-dom";
// import "../LoginForm.css";
import "./UpdateProfileSP.css";
import { FaEye, FaEyeSlash } from "react-icons/fa6";
const UpdateProfileSP=()=>{
    const userId = sessionStorage["userId"];
    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();
   
    const userData = JSON.parse(localStorage.getItem("serviceProvider"));
    const [formData, setFormData] = useState({
        NameFirst: userData.nameFirst,
        NameLast: userData.nameLast,
        Username: userData.username,
        Password: userData.password,
        PhoneNumber: userData.phoneNumber,
        Address: userData.address,
        Skills:userData.skills,
        Wages:userData.wages
      });
console.log(formData)
     
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
        const response = await axios.post('http://localhost:5020/api/ServiceProvider/ServiceUpdate', formData).then(() => {
          navigate("/data");
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
              <select
                className="form-control"
                id="skills"
                name="Skills"
                value={formData.Skills}
                onChange={handleChange}
                defaultValue=""
                required
              >
                <option value="" disabled>Select a service</option>
                <option value="Plumber">Plumber</option>
                <option value="Carpenter">Carpenter</option>
                <option value="Painter">Painter</option>
                <option value="Pest Controller">Pest Controller</option>
                <option value="Gardener">Gardener</option>
                <option value="Electrician">Electrician</option>
                <option value="Cleaner">Cleaner</option>

            
              </select>
              <input
                type="text"
                placeholder="Charges"
                name="Wages"
                value={formData.Wages}
                onChange={handleChange}
                pattern="^[1-9][0-9]*$"
                required
              />
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
export default UpdateProfileSP;











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
