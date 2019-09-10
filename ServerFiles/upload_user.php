<?php
$response['error'] = true;
$response['message'] = "Unknown error";

include "db_connection.php";

if($_SERVER['REQUEST_METHOD'] == "POST"){
    //Variables
    $username = $_POST['username'];
    $new_email = $_POST['new_email'];
    $current_password = $_POST['current_password'];
    $new_password = $_POST['new_password'];
    $xp = $_POST['xp'];
    $level = $_POST['level'];

    //Check if user exists and check password
    $query = "SELECT * FROM users WHERE username = '{$username}'";
    $result = $conn->query($query);
    if($result->num_rows > 0){
        $password_hash = $result->fetch_assoc()['password'];
        if(password_verify($current_password, $password_hash)){
            //if user is authenticated, then update information
            $new_password_hash = $password_hash($new_password, PASSWORD_DEFAULT);
            $query = "UPDATE users SET" .
                "email = '{$new_email}'," .
                "password = '{$new_password_hash}'".
                "xp = {$xp}".
                "level = {$level}".
                "WHERE username = '{$username}'";
            $conn->query($query);
            if(!$conn->error){
                $response['error']= false;
                $response['message'] = "User updated successfully.";
            }else{
                $response['error'] = true;
                $response['message'] = "Error updating user details: " . $conn->error;
            }
        }else{
            $response['error'] = true;
            $response['message'] = "Username or password is incorrect.";
        }
    }else{
        $response['error'] = true;
        $response['message'] = "Username or password is incorrect.";
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}

echo json_encode($response);
