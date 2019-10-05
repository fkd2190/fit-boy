<?php
$response['error'] = false;
$response['message'] = "Unknown Error";
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $response['message'] = "";
    $username = $_POST['username'];
    $friend_username = $_POST['friend_username'];
    $user_id = $friend_id = "";

    //get user_id
    $query = "SELECT user_id FROM users WHERE username = '{$username}'";
    $result = $conn->query($query);
    if($result->num_rows > 0){
        $user_id = $result->fetch_assoc()['user_id'];
    }else{
        $response['error'] = true;
        $response['message'] .= "First username not found in the database. ";
    }

    if(!$response['error']) {
        //get friend_id
        $query = "SELECT user_id FROM users WHERE username = '{$friend_username}'";
        $result = $conn->query($query);
        if ($result->num_rows > 0) {
            $friend_id = $result->fetch_assoc()['user_id'];
        } else {
            $response['error'] = true;
            $response['message'] .= "Friend username not found in the database. ";
        }
    }

    //remove from database
    $query = "DELETE FROM friends WHERE (user_id = {$user_id} AND friend_id = {$friend_id}) OR (user_id = {$friend_id} AND friend_id = {$user_id})";
    $result = $conn->query($query);
    if(!$conn->error){
        $response['error'] = false;
        $response['message'] = "Friends successfully removed.";
    }else{
        $response['error'] = true;
        $response['message'] = "Database Error: " . $conn->error;
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);


