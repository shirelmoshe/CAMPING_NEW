import React from "react";
import "./r.css";
import { useState } from "react";
import { addSale } from "./../servers/servers";

export const ProductData = ({ CampaignName, Product, Price, productsId }) => {
  console.log(` ProductName : ${CampaignName}`);
  console.log(` unitsInStock : ${Product}`);
  console.log(` unitsInStock : ${productsId}`);
  const [userMessage, setUserMessage] = useState({
    buyerName: "",
    cellphoneNumber: "",
    Email: "",
    buyerAddress: "",
    CompanyName: "",
    productsId,
  });
  const [errorMessage, setErrorMessage] = useState("");
  const [isDisabled, setIsDisabled] = useState(false);
  var productsId = productsId;
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
    await addSale(json);
    setUserMessage({});
    document.querySelectorAll("input").forEach((input) => (input.value = ""));
  };

  const validateInput = () => {
    if (!userMessage.buyerName) {
      return "buyer Name is required";
    }
    if (!userMessage.cellphoneNumber) {
      return "cellphoneNumber is required";
    }
    if (!userMessage.Email) {
      return "Email is required";
    }
    if (!userMessage.buyerAddress) {
      return "buyer Address is required";
    }
    if (!userMessage.CompanyName) {
      return "Company Name Name is required";
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
      <div className="card">
        <div className="card product-container">
          <div className="card-body">
            <h2 className="card-title">{Product}</h2>
            <h5>Unit Price: {Price}</h5>
            <h5>CompanyName: {CampaignName}</h5>
          </div>
          <div className="input-group mb-3">
            <span className="input-group-text">name</span>
            <input
              className="form-control"
              type="text"
              onChange={(o) => {
                setUserMessage({ ...userMessage, buyerName: o.target.value });
              }}
            />
          </div>
          <div className="input-group mb-3">
            <span className="input-group-text">cellphone Number</span>
            <input
              className="form-control"
              type="text"
              onChange={(o) => {
                setUserMessage({
                  ...userMessage,
                  cellphoneNumber: o.target.value,
                });
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
                setUserMessage({ ...userMessage, Email: o.target.value });
              }}
            />
            <div id="emailHelp" class="form-text">
              We'll never share your email with anyone else.
            </div>
          </div>

          <div className="input-group mb-3">
            <span className="input-group-text">Address</span>
            <input
              className="form-control"
              type="text"
              onChange={(o) => {
                setUserMessage({
                  ...userMessage,
                  buyerAddress: o.target.value,
                });
              }}
            />
          </div>
          <div className="input-group mb-3">
            <span className="input-group-text">CompanyName</span>
            <input
              className="form-control"
              type="text"
              onChange={(o) => {
                setUserMessage({
                  ...userMessage,
                  CompanyName: o.target.value,
                });
              }}
            />
          </div>
        </div>
        <button className="btn btn-secondary" onClick={handleAddMessage}>
          Send
        </button>
      </div>
    </div>
  );
};
