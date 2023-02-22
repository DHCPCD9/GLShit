#version 330 core
layout(location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor; // the color variable has attribute position 1

out vec3 ourColor; // output a color to the fragment shader

//Positions
uniform vec3 uPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

//Scale default is 1
uniform vec3 uScale;


void main()
{
    gl_Position = vec4(uPos + aPos, 1.0f);
    ourColor = aColor;
}      