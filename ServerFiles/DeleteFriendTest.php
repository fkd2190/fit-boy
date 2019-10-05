<?php


use PHPUnit\Framework\TestCase;

class DeleteFriendTest extends TestCase
{
    protected function setUp(): void
    {
        //Add two users to the database to test
        include('db_connection.php');
        $hash = password_hash('password', PASSWORD_DEFAULT);
        $query = "INSERT INTO users (username, password, email)
                VALUES ('joebloggs', '{$hash}', 'joebloggs@example.com'),
                ('joebloggs2', '{$hash}', 'joebloggs2@example.com')";
        $conn->query($query);

        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "joebloggs2";
        include "add_friend.php";
    }

    protected function tearDown(): void
    {
        //Remove the users from the database
        include('db_connection.php');
        $query = "DELETE FROM users WHERE username = 'joebloggs' OR username = 'joebloggs2'";
        $conn->query($query);
    }

    public function test_remove_existing_friend(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "joebloggs2";

        //Run file
        ob_start();
        include('delete_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertFalse($response['error']);
    }

    public function test_remove_non_existing_friend(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "SomeNonExistingFriend";

        //Run file
        ob_start();
        include('delete_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_remove_non_existing_user(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "SomeNonExistingUser";
        $_POST['friend_username'] = "joebloggs2";

        //Run file
        ob_start();
        include('delete_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_invalid_http_request_method(): void
    {
        $_SERVER["REQUEST_METHOD"] = "GET";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "joebloggs2";

        //Run file
        ob_start();
        include('delete_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }
}
