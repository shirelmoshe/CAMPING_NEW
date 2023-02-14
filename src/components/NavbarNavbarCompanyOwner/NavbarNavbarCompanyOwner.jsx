import React from "react";
import { Link } from "react-router-dom";
import "./main.css";

export const NavbarCompanyOwner = () => {
  return (
    <>
      <nav className="navbar">
        <div className="navbar-container">
          <ul className="navbar_menu">
            <li className="navbar_item">
              <Link to="/CompanyOwnerUser" className="navbar_links">
                profile
              </Link>
            </li>
            <li className="navbar_item">
              <Link to="/" className="navbar_links">
                Home
              </Link>
            </li>

            <li className="navbar_item">
              <Link to="/Campaigns" className="navbar_links">
                Campaign
              </Link>
            </li>
            <li className="navbar_item">
              <Link to="/Products" className="navbar_links">
                Products
              </Link>
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
};

export default NavbarCompanyOwner;
