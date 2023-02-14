import { addUserServers } from "../../servers/servers";
import "./SignUp.css";
import { GetRoles } from "../../servers/servers";
import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";

export const SingUp = () => {
  const { user } = useAuth0();

  let user_id = user.sub;
  const handleRoles = async () => {
    let roles = await GetRoles(user_id);
  };
  useEffect(() => {
    handleRoles();
  }, []);

  const [userMessage, setUserMessage] = useState({
    userName: "",
    cellphoneNumber: "",
    email: "",
    UserType: "",
    user_id,
  });
  const [errorMessage, setErrorMessage] = useState("");
  const [isDisabled, setIsDisabled] = useState(false);

  const handleAddMessage = async () => {
    let error = validateInput();
    if (error) {
      setErrorMessage(error);
      setIsDisabled(true);
      return;
    }
    setErrorMessage("");
    setIsDisabled(false);
    let json = userMessage;
    await addUserServers(json);
    setUserMessage({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  const validateInput = () => {
    if (!userMessage.userName) {
      return "userName is required";
    }
    if (!userMessage.cellphoneNumber) {
      return "cellphoneNumber is required";
    }
    if (!userMessage.email) {
      return "email is required";
    }
    if (!userMessage.UserType) {
      return "UserType is required";
    }

    if (
      !/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(userMessage.email)
    ) {
      return "Email is not valid.";
    }
    return "";
  };
  const handleSelectChange = (event) => {
    setUserMessage({ ...userMessage, UserType: event.target.value });
  };
  return (
    <div className="student-inputs ">
      <p className="error-message">{errorMessage}</p>
      <div className="input-group mb-3">
        <span className="input-group-text">user Name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, userName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">cellphoneNumber</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, cellphoneNumber: o.target.value });
          }}
        />
      </div>
      <div class="mb-3">
        <label for="exampleInputEmail1" class="form-label">
          Email address
        </label>
        <input
          type="email"
          class="form-control"
          id="exampleInputEmail1"
          aria-describedby="emailHelp"
          onChange={(o) => {
            setUserMessage({ ...userMessage, email: o.target.value });
          }}
        />
        <div id="emailHelp" class="form-text">
          We'll never share your email with anyone else.
        </div>
      </div>

      <div>
        <select onChange={handleSelectChange}>
          <option value="Association representative">
            Association representative
          </option>
          <option value="business company">business company</option>
          <option value="social activist">social activist</option>
        </select>
      </div>
      <button className="btn btn-secondary" onClick={handleAddMessage}>
        Send
      </button>
    </div>
  );
};
