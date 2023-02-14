import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { getsocialActivistDataById } from "../../servers/servers";

export const SocialActivistUser = ({ user }) => {
  //const { user_id } = useParams();
  //console.log(userArr.user_id);
  const [userArr, setUser] = useState(null);
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);

  const initProductData = async () => {
    try {
      const users = await getsocialActivistDataById(user);
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
          <li className="list-group-item">
            <button type="button" className="btn btn-info">
              <Link to="/CampaignSupport" className="navbar_links">
                Campaign Support
              </Link>
            </button>
          </li>
          <li className="list-group-item">
            <button type="button" className="btn btn-info">
              <Link to="/MoneyTracking" className="navbar_links">
                Money Tracking
              </Link>
            </button>
          </li>
        </ul>
      </div>
    );
  }
};
