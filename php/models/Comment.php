<?php
/**
 * Created by JetBrains PhpStorm.
 * User: jferrara
 * To change this template use File | Settings | File Templates.
 */
class Comment extends ActiveRecord\Model
{
    static $attr_accessible = array('user_id','post_id','body'); // only for mass assignment.

    static $belongs_to = array(
        array('post'),
        array('user')
    );

    static $validates_presence_of = array(
        array('post_id'),
        array('user_id'),
        array('body')
    );

    static $validate_size_of = array(
            array('body','minimum'=> 12, 'too_short'=>'You have to type more.')
    );
}
