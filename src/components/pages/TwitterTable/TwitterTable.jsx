import React, { useState, useEffect } from "react";
import { getTwitterTable } from "../../servers/servers";
import { TwitterCard } from "../../Twitter/TwitterCard";

export const TwitterTable = () => {
  const [twitterTable, setTwitterTable] = useState([]);
  const [filter, setFilter] = useState("");

  const initTwitterTableInfo = async () => {
    let response = await getTwitterTable();
    if (response && typeof response === "object") {
      let CampaignsArr = Object.values(response);
      setTwitterTable(CampaignsArr);
    } else {
      console.log("error");
    }
  };

  useEffect(() => {
    initTwitterTableInfo();
  }, []);

  const handleFilter = (e) => {
    setFilter(e.target.value);
  };

  const filteredTwitterTable = twitterTable.filter((card) => {
    return (
      card.associationName.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.hashtag.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.email.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.userName.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.twitterUsername.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.CampaignName.toLowerCase().indexOf(filter.toLowerCase()) !== -1 ||
      card.userMoney.toString().indexOf(filter) !== -1
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
            <th scope="col">associationName</th>
            <th scope="col">hashtag</th>
            <th scope="col">email</th>
            <th scope="col">userName</th>
            <th scope="col">twitterUsername</th>
            <th scope="col">CampaignName</th>
            <th scope="col">userMoney</th>
          </tr>
        </thead>
        <tbody className="table table-striped">
          {filteredTwitterTable.map((response) => {
            const {
              userId,
              associationName,
              hashtag,
              email,
              userName,
              twitterUsername,
              CampaignName,
              userMoney,
            } = response;
            return (
              <TwitterCard
                key={userId}
                associationName={associationName}
                hashtag={hashtag}
                email={email}
                userName={userName}
                twitterUsername={twitterUsername}
                CampaignName={CampaignName}
                userMoney={userMoney}
              ></TwitterCard>
            );
          })}
        </tbody>
      </table>
    </>
  );
};
