MovecControlTrack
-----------------

This is an example of fixing issues found in motion vector generation with the
skinned mesh renderer.

![NG](https://i.imgur.com/Z364VbK.gif)
![OK](https://i.imgur.com/xoJOXUw.gif)

(left: before, right: after)

There are two issues with these motion vectors:

1. Initial frame spike. It generates irregularly long motion vectors in the
   first frame from activation.
2. Wrong depth bias. You can see the arms are drawn over the body (they're
   actually covered by the body).

In this example, these issue are solved by using a custom timeline track.
