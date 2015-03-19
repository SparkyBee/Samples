<?php
/**
 * Created by JetBrains PhpStorm.
 * User: jferrara
 * Date: 4/30/12
 * Time: 10:55 AM
 * To change this template use File | Settings | File Templates.
 */
class Post extends ActiveRecord\Model
{
    static $belongs_to = array(
        array('user')
    );

    static $has_many = array(
        array('comments')
    );




}
