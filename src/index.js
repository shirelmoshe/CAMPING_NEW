import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { App } from "./App";
import "../node_modules/bootstrap/dist/css/bootstrap.css";
import { Auth0Provider } from "@auth0/auth0-react";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <Auth0Provider
      domain="dev-s8ct5eftw1k36wbf.us.auth0.com"
      clientId="IB6Wjk7VL0lL2BnPR8thRwlUwa9d04Z1"
      redirectUri={window.location.origin}
    >
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </Auth0Provider>
  </React.StrictMode>
);
