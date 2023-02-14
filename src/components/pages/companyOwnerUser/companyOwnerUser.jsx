import React from "react";
import "./companyOwnerUser.css";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import { useState } from "react";
import { getCompanyOwnerUserDataById } from "../../servers/servers";

export const CompanyOwnerUser = ({ user }) => {
  const [userArr, setUser] = useState(null);
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);

  const initProductData = async () => {
    try {
      const users = await getCompanyOwnerUserDataById(user);
      setUser(users);
      setIsLoaded(true);
    } catch (error) {
      setError(error);
      setIsLoaded(true);
      console.log("error");
    }
  };
  useEffect(() => {
    initProductData();
  }, []);

  if (error) {
    return <div>Error: {error.message}</div>;
  } else if (!isLoaded) {
    return <div>Loading...</div>;
  } else if (!userArr) {
    return <div>User not found</div>;
  } else {
    return (
      <div className="card">
        <div className="card-body">
          {userArr && <h5 className="card-title">{userArr.userName}</h5>}
        </div>
        <ul className="list-group list-group-flush">
          {userArr && <li className="list-group-item">{userArr.email}</li>}
          {userArr && (
            <li className="list-group-item">{userArr.cellphoneNumber}</li>
          )}
          {userArr && <li className="list-group-item">{userArr.UserType}</li>}
          <li class="list-group-item">
            <button type="button" class="btn btn-info">
              <Link to="/donation" className="navbar_links">
                Product donation
              </Link>
            </button>
          </li>
          <li class="list-group-item">
            <button type="button" class="btn btn-info">
              <Link to="/Shipping" className="navbar_links">
                Shipment report
              </Link>
            </button>
          </li>
        </ul>
      </div>
    );
  }
};
