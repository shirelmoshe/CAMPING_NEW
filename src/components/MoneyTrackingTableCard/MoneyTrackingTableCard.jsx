import React from "react";

export const MoneyTrackingTableCard = ({
  user_id,
  associationName,
  hashtag,
  userName,
  userMoney,
}) => {
  return (
    <>
      <tr>
        <td>{associationName}</td>
        <td>{hashtag}</td>
        <td>{userName}</td>
        <td>{userMoney}</td>
      </tr>
    </>
  );
};
