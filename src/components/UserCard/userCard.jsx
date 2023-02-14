import React from "react";

export const CardUser = ({
  userId,
  userName,
  cellphoneNumber,
  email,
  UserType,
}) => {
  return (
    <>
      <tr>
        <td>{userName}</td>
        <td>{cellphoneNumber}</td>
        <td>{email}</td>
        <td>{UserType}</td>
      </tr>
    </>
  );
};
