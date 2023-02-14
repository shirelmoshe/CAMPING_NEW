import React, { useState } from "react";
import { addDonation } from "../../servers/servers";
import "./Donation.css";

export const Donation = (props) => {
  const [userMessage, setUserMessage] = useState({
    CompanyName: "",
    Product: "",
    CampaignName: "",
    Price: "",
    Email: "",
    Quantity: "",
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
    await addDonation(json);
    setUserMessage({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  const validateInput = () => {
    if (!userMessage.CompanyName) {
      return "CompanyName is required";
    }
    if (!userMessage.Product) {
      return "Product is required";
    }
    if (!userMessage.CampaignName) {
      return "CampaignName is required";
    }
    if (!userMessage.Price) {
      return "URI is required";
    }
    if (!userMessage.Price) {
      return "Campaign Name is required";
    }
    if (!userMessage.Email) {
      return "Email Name is required";
    }
    if (!userMessage.Quantity) {
      return "Quantity Name is required";
    }
    if (
      !/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(userMessage.Email)
    ) {
      return "Email is not valid.";
    }
    return "";
  };

  return (
    <div className="student-inputs ">
      <p className="error-message">{errorMessage}</p>
      <div className="input-group mb-3">
        <span className="input-group-text">CompanyName</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, CompanyName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Product</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, Product: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">CampaignName</span>
        <input
          className="form-control"
          type="email"
          onChange={(o) => {
            setUserMessage({ ...userMessage, CampaignName: o.target.value });
          }}
        />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text">Price</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, Price: o.target.value });
          }}
          maxLength={300}
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
            setUserMessage({ ...userMessage, Email: o.target.value });
          }}
          maxLength={300}
        />
        <div id="emailHelp" class="form-text">
          We'll never share your email with anyone else.
        </div>
      </div>

      <div className="input-group mb-3">
        <span className="input-group-text">Quantity</span>
        <input
          className="form-control"
          type="text"
          onChange={(o) => {
            setUserMessage({ ...userMessage, Quantity: o.target.value });
          }}
        />
      </div>
      <button className="btn btn-secondary" onClick={handleAddMessage}>
        Send
      </button>
    </div>
  );
};
