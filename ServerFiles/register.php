<?php
$response['error'] = true;
$response['message'] = "Unknown Error";
include "db_connection.php";

if ($_SERVER["REQUEST_METHOD"] == "POST"){
    $response['error'] = false;
    $response['message'] = "";
    //Set local variables from POST data
    $username = $_POST['username'];
    $email = $_POST['email'];
    $password = $_POST['password'];

    //Check if username exists in database
    $query = "SELECT * FROM users WHERE username = '{$username}'";
    $results = $conn->query($query);
    if($results->num_rows > 0){
        $response['error'] = true;
        $response['message'] = "Username already exists.";
    }
    //Check if email exists in database
    $query = "SELECT * FROM users WHERE email = '{$email}'";
    $results = $conn->query($query);
    if($results->num_rows > 0){
        $response['error'] = true;
        $response['message'] .= "\nEmail already exists.";
    }


    //Check validity of fields
    //--username
    if(strlen($username) <= 0 || strlen($username) > 25){
        $response['error'] = true;
        $response['message'] .= "Username can't be empty or greater than 25 characters\n";
    }

    //--email
    if(!filter_var($email, FILTER_VALIDATE_EMAIL)){
        $response['error'] = true;
        $response['message'] .= "Email is not in a valid format.\n";
    }else if(strlen($email) > 255){
        $response['error'] = true;
        $response['message'] .= "Email is too long. Please enter an email address up to 255 characters.\n";
    }

    //--password
    if(strlen($password) <= 0){
        $response['error'] = true;
        $response['message'] .= "Password can't be empty.\n";
    }


    //Add user to database
    if(!$response['error']){
        //Hash Password
        $hashed_password = password_hash($password, PASSWORD_DEFAULT);
        //Insert user to database
        $query = "INSERT INTO users (username, email, password, xp, level)
            VALUES('{$username}', '{$email}', '{$hashed_password}', 0, 0)";
        $conn->query($query);
        if($conn->error){
            $response['error'] = true;
            $response['message'] = "Error registering user: " . $conn->error;
        }else{
            $response['error'] = false;
            $response['message'] = "User added to database.";
        }
    }

}else{
    $response['error'] = true;
    $response['message'] = "Invalid request method used. Please use POST";
}
echo json_encode($response);



