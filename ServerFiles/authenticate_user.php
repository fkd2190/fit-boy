<?php
$response['error'] = true;
$response['message'] = "Unknown Error";
$response['user'] = null;
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    //Get data from post variables
    $username = $_POST['username'];
    $password = $_POST['password'];

    //Get Record from database
    $query = "SELECT * FROM users WHERE username='{$username}'";
    $result = $conn->query($query);
    if($result->num_rows <=0){
        $response['error'] = true;
        $response['message'] = "Username or password incorrect.";
    }else{
        $user = $result->fetch_assoc();
        $password_hash = $user['password'];
        if(password_verify($password, $password_hash)){
            $response['error'] = false;
            $response['message'] = "User authenticated successfully.";
            $response['user'] = $user;
            unset($response['user']['password']);
        }else{
            $response['error'] = true;
            $response['message'] = "Username or password incorrect.";
        }
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);



