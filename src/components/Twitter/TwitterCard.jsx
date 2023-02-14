import React from "react";

export const TwitterCard = ({
  userId,
  associationName,
  hashtag,
  email,
  userName,
  twitterUsername,
  CampaignName,
  userMoney,
}) => {
  return (
    <>
      <tr>
        <td>{associationName}</td>
        <td>{hashtag}</td>
        <td>{email}</td>
        <td>{userName}</td>
        <td>{twitterUsername}</td>
        <td>{CampaignName}</td>
        <td>{userMoney}</td>
      </tr>
    </>
  );
};
