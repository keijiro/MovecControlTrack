MovecControlTrack
=================

This is an example of fixing issues found in motion vector generation with the
skinned mesh renderer.

Issues
------

![NG](https://i.imgur.com/Z364VbK.gif)

Two issues can be observed in this GIF:

1. Initial frame spike. It generates irregularly large motion vectors in the
   first frame from its activation.
2. Wrong depth bias. The arms are drawn over the body despite being covered by
   the body.

Results
-------

These issue are solved by using a custom timeline track that controls the
motion vector generation mode and adjusts the depth bias.

![OK](https://i.imgur.com/xoJOXUw.gif)
