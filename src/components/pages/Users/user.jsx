import React, { useState, useEffect } from "react";
import { getUsersData } from "../../servers/servers";
import { CardUser } from "../../UserCard/userCard";

export const UsersTable = () => {
  const [UserInfo, setUserInfo] = useState([]);
  const [filter, setFilter] = useState("");

  const initTwitterTableInfo = async () => {
    let response = await getUsersData();
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setUserInfo(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initTwitterTableInfo();
  }, []);

  const handleFilter = (e) => {
    setFilter(e.target.value.toLowerCase());
  };

  const filterUserTable = UserInfo.filter((card) => {
    return (
      (typeof card.userId === "string" &&
        card.userId.toLowerCase().indexOf(filter) !== -1) ||
      (typeof card.userName === "string" &&
        card.userName.toLowerCase().indexOf(filter.toLowerCase()) !== -1) ||
      (typeof card.cellphoneNumber === "string" &&
        card.cellphoneNumber.toLowerCase().indexOf(filter.toLowerCase()) !==
          -1) ||
      (typeof card.email === "string" &&
        card.email.toLowerCase().indexOf(filter.toLowerCase()) !== -1) ||
      (typeof card.UserType === "string" &&
        card.UserType.toLowerCase().indexOf(filter.toLowerCase()) !== -1)
    );
  });

  return (
    <>
      <div className="filter-container">
        <input
          type="text"
          placeholder="Filter"
          value={filter}
          onChange={handleFilter}
        />
      </div>
      <table className="table table-striped">
        <thead className="table table-striped">
          <tr>
            <th scope="col">userName</th>
            <th scope="col">cellphoneNumber</th>
            <th scope="col">email</th>
            <th scope="col">UserType</th>
          </tr>
        </thead>
        <tbody className="table table-striped">
          {filterUserTable.map((response) => {
            const { userId, userName, cellphoneNumber, email, UserType } =
              response;
            return (
              <CardUser
                key={userId}
                userName={userName}
                cellphoneNumber={cellphoneNumber}
                email={email}
                UserType={UserType}
              ></CardUser>
            );
          })}
        </tbody>
      </table>
    </>
  );
};
