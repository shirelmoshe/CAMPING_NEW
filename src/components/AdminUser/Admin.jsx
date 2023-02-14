import React from "react";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import { useState } from "react";
import { getAdminDataById } from "../servers/servers";

export const AdminUser = ({ user }) => {
  const [userArr, setUser] = useState(null);
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);

  const initProductData = async () => {
    try {
      const users = await getAdminDataById(user);
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
              <Link to="/User" className="navbar_links">
                User report
              </Link>
            </button>
          </li>
          <li class="list-group-item">
            <button type="button" class="btn btn-info">
              <Link to="/TwitterTable" className="navbar_links">
                Twitter report
              </Link>
            </button>
          </li>
          <li class="list-group-item">
            <button type="button" class="btn btn-info">
              <Link to="/Sales" className="navbar_links">
                Purchase report
              </Link>
            </button>
          </li>
          <li class="list-group-item">
            <button type="button" class="btn btn-info">
              <Link to="/CampaingsTable" className="navbar_links">
                Campaigns report
              </Link>
            </button>
          </li>
        </ul>
      </div>
    );
  }
};
