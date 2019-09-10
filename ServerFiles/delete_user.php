<?php
include "db_connection.php";
$response['error'] = true;
$response['message'] = "Unknown error";
if($_SERVER['REQUEST_METHOD'] == "POST"){
    $conn->query("DELETE FROM users WHERE username = '{$_POST['username']}'");
    if($conn->error){
        $response['error'] = true;
        $response['message'] = "Error deleting user from database: " . $conn->error;
    }else{
        $response['error'] = false;
        $response['message'] = "User successfully deleted.";
    }
}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);
$conn->query("DELETE FROM users WHERE username = '{$_POST['username']}'");