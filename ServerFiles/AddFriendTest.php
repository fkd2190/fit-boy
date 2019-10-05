<?php


use PHPUnit\Framework\TestCase;

class AddFriendTest extends TestCase
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
    }

    protected function tearDown(): void
    {
        //Remove the users from the database
        include('db_connection.php');
        $query = "DELETE FROM users WHERE username = 'joebloggs' OR username = 'joebloggs2'";
        $conn->query($query);
    }

    public function test_add_valid_friend(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "joebloggs2";

        //Run file
        ob_start();
        include('add_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertFalse($response['error']);
    }

    public function test_friend_not_found(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "joebloggs";
        $_POST['friend_username'] = "someOtherFriend";

        //Run file
        ob_start();
        include('add_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }

    public function test_user_not_found(): void
    {
        $_SERVER["REQUEST_METHOD"] = "POST";
        $_POST['username'] = "someUserNotInTheDatabase";
        $_POST['friend_username'] = "joebloggs2";

        //Run file
        ob_start();
        include('add_friend.php');
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
        include('add_friend.php');
        $response = json_decode(ob_get_clean(), true);
        $this->assertTrue($response['error']);
    }
}
