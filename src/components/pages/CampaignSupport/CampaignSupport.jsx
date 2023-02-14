import React, { useState } from "react";
import { addSupportServers } from "../../servers/servers";

export const CampaignSupport = (props) => {
  const [userMessage, setUserMessage] = useState({
    associationName: "",
    hashtag: "",
    email: "",
    userName: "",
    twitterUsername: " ",
    CampaignName: "",
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
    await addSupportServers(json);
    setUserMessage({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  const validateInput = () => {
    if (!userMessage.associationName) {
      return "Association Name is required";
    }
    if (!userMessage.hashtag) {
      return "Hashtag is required";
    }
    if (!userMessage.email) {
      return "Email is required";
    }
    if (!userMessage.userName) {
      return "URI is required";
    }
    if (!userMessage.twitterUsername) {
      return "twitter User name is required";
    }
    if (!userMessage.CampaignName) {
      return "Campaign Name is required";
    }
    if (
      !/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(userMessage.email)
    ) {
      return "Email is not valid.";
    }
    return "";
  };

  return (
    <div className="student-inputs ">
      <p className="error-message">{errorMessage}</p>
      <div className="input-group mb-3">
        <span className="input-group-text">association Name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, associationName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">hashtag</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, hashtag: o.target.value });
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

      <div className="input-group mb-3">
        <span className="input-group-text">user Name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, userName: o.target.value });
          }}
          maxLength={300}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">twitter User name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, twitterUsername: o.target.value });
          }}
          maxLength={300}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Campaign Name</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, CampaignName: o.target.value });
          }}
        />
      </div>
      <button className="btn btn-secondary" onClick={handleAddMessage}>
        Send
      </button>
    </div>
  );
};
