import React, { useState, useEffect } from "react";
import { getMoneyTrackingTable } from "../../servers/servers";
import { MoneyTrackingTableCard } from "../../MoneyTrackingTableCard/MoneyTrackingTableCard";

export const MoneyTrackingTable = ({ user }) => {
  const [MoneyTrackingTable, setMoneyTrackingTable] = useState([]);

  const initTwitterTableInfo = async () => {
    try {
      console.log(user);
      let response = await getMoneyTrackingTable(user);

      if (response && typeof response === "object") {
        let CampaignsArr = Object.values(response);
        setMoneyTrackingTable(CampaignsArr);
      }
    } catch (error) {
      console.log("error");
    }
  };

  useEffect(() => {
    initTwitterTableInfo();
  }, []);

  return (
    <>
      <table class="table table-striped">
        <thead className="table table-striped">
          <tr>
            <th scope="col">associationName</th>
            <th scope="col">hashtag</th>
            <th scope="col">userName</th>
            <th scope="col">userMoney</th>
          </tr>
        </thead>
        <tbody class="table table-striped">
          {MoneyTrackingTable &&
            MoneyTrackingTable.map((response) => {
              const { user_id, associationName, hashtag, userName, userMoney } =
                response;
              return (
                <MoneyTrackingTableCard
                  key={user_id}
                  associationName={associationName}
                  hashtag={hashtag}
                  userName={userName}
                  userMoney={userMoney}
                ></MoneyTrackingTableCard>
              );
            })}
        </tbody>
      </table>
    </>
  );
};
