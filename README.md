# Neural Networks

Introduction
------------
If this is your first foray into Neural Networks, welcome!  I hope you enjoy yourself as much as I have.

This project is my first attempt at creating an application that allows for quick interactions with a basic neural network.  I find that I learn better when I have something quick and easy to tinker with that allows me to analyze pieces of a concept at my own pace.  Hopefully, this will help some of you that learn in a similar manner.

This project is written in C#. 

![Alt text](ScreenShot.PNG?raw=true "Optional Title")
What is a Neural Network?
-----
Great question!  

A Neural Network can be thought of as an artificial representation of a brain.  A Neural Network is made up of nodes (or neurons) that are interconnected, much like they are in the brain.  The network can have any number (N) of inputs and any number (M) of outputs.  In between the inputs and outputs are a series of "hidden" neurons that make up the hidden layers of the network.  These hidden layers provide the meat of the network and allow for some of the neat functionalities we can get out of a Neural Network.

What are the Parts of a Neural Network?
--------
Before explaining the pieces of a neural network, it might be helpful to start with an example.  

Building off of [this excellent article from 2013](http://www.codingvision.net/miscellaneous/c-backpropagation-tutorial-xor), let's use the concept of Exclusive Or (XOR).  XOR will output true when the inputs differ:

| Input A | Input B | Output  |
|:-----:|:---------:|:-----:|
| false | false | false |
| false | true | true |
| true | false | true |
| true | true | false |

Considering this, let's break down a Neural Network into its three basic parts:

1. The Inputs
	* These are the inputs into the Neural Network.  From the XOR example above, the inputs would be Input A and Input B.
	* Each input can be considered a Neuron whose output is the initial input value. 
2. The Hidden Layers
	* This is the meat of the Neural Network.  This is where the magic happens.  The Neurons in this layer are assigned weights.  These weights start off fairly random, but as the network is "trained" (discussed below), the weights are adjusted in order to make the Neural Network's output closer to the expected result.
3. The Outputs
	* These are the outputs from the system. From the XOR example above, the output from the system would be either 'true' or 'false'.  In the Neural Network, the Outputs are the last line of Neurons.  These Neurons are also assigned a weight and are "fed" by the Neurons in the hidden layer.  

Using the XOR example, if we were to give our Neural Network the inputs 'true' and 'false' we would expect the system to return 'true'. 

How Does it Work?
-------
Because I love examples, here's another: 

| Input A | Input B | Input C | Output  |
|:-----:|:---------:|:-----:|:------|
| true | false | false | true |
| false | true | true | false |
| true | fase | true | false |
| true | true | true | true |

In the above table, we can infer the following patterns:

1. The output is true if the number of inputs set to true is odd OR the number of inputs set to false is even.
2. The output is false if the number of inputs set to true is even OR the number of inputs set to false is odd.

The job of the Neural Network is to try and figure out that pattern.  It does this via training.

#### How Do We Train the Neural Network?

Training the Neural Network is accomplished by giving it a set of input data and the expected results for those inputs.  This data is then continuously run through the Neural Network until we can be reasonably sure that it has a grasp of the patterns present in that data. 

In this project, the Neural Network is trained via two very common Neural Network training methods:

1. Back-Propagation
	* After each set of inputs is run through the system and an output generated, that output is validated against the expected output.  
	* The percentage of error that results is then propagated backwards (hence the name) through the Hidden Layers of the Neural Network.  This adjusts the weights assigned to each Neuron in the Hidden Layers.  
	* Ideally, each Back-Propagation will bring the Neural Network's output closer to the expected output of the provided inputs.
2. Biases
	* Biases allow us to modify our activation function (discussed below) in order to generate a better output for each neuron.  
	* [See Here](http://stackoverflow.com/questions/2480650/role-of-bias-in-neural-networks) for an excellent explanation as to what a bias does for a Neural Network.  

#### What defines a Neuron's Output?

A Neuron's output is defined by an [Activation Function](https://en.wikipedia.org/wiki/Activation_function).

In our case, we are using a [Sigmoid Function](https://en.wikipedia.org/wiki/Sigmoid_function) to define each Neuron's output.  The Sigmoid function will convert any value to a value between 0 and 1.  In the Neural Network, the Sigmoid functions will be used to generate initial weights and to help update percent errors.  

How Do I Use this Program?
------

This program is fairly simple to use.  

When it starts, you will be presented with the option to load the Neural Network configuration from a data file.  The file is currently set up to use the common XOR example.  This means that the network will be initialized with the following:
* Number of Inputs: 2
* Number of Hidden Neurons: 10
* Number of Outputs: 1

Test Data:

| Input A | Input B | Output |
|:-----:|:---------:|:-----:|
| 0 | 0 | 0 |
| 0 | 1 | 1 |
| 1 | 0 | 1 |
| 1 | 1 | 0 |

To change the file (data.txt):

1. Make the first line as follows: {{number of inputs}} {{number of hidden neurons}} {{number of outputs}}
2. Add any number of lines representing your data.  Simply put in your inputs (space delmited) followed by your expected outputs (space delimited).

Alternatively, you can manually type in your own manual configuration and data sets.  There is a fair amount of validation in place to guide you through the process.

Once the data sets are...set... the program will begin training the Neural Network.  Currently, the program is set to train the network for 5000 epochs (iterations).  This is a bit of overkill.  Feel free to alter this value via the constant in the program. 

After the Neural Network is trained, you will be asked to enter input data and verify the output.  If the output is wrong, you can enter the correct output.  The network will then be "retrained".  If the input is correct, the network will be "encouraged".  

If you used the XOR example and gave inputs of 0 and 1, you might see output values like: 0.985902....

This is the Neural Network attempting to get as close as possible to the expected output of 1.  As the network is encouraged, this number will get higher. 

What's Next?
-----

I'm going to continue learning more about Neural Networks.  This program represents my Saturday of research and coding.  My understanding of Neural Networks is FAR from complete and I still feel unknowledgeable in many areas.  

For the program, I will continue adding bells and whistles such as other training methods.  I would also like to find a way to adequately represent the Neural Network and its weights after it has been trained.  I know I have done a woeful job of explaining that part and I feel that a reprentation of the network throughout the training process would be immensely helpful. 

Code Considerations
---
Feel free to use any part of this program.  

#### Reusability
The Network and its supporting classes are self-contained, meaning that the "UI" portion of the program only serves to gather the necessary information to instantiate the Network class and its supporting classes.  You could theoretically take the Network and supporting classes and bring it into your own application with little to no modification. The network only requires the number of inputs, number of hidden neurons and the number of outputs to be instantiated. 

#### What's With the Useless 'Layer' class?
I put this in with the future in mind.  There are Neural Networks in which some Hidden Neurons aren't mapped backwards, but are mapped forwards.  Having the layers will allow me to add this functionality faster in the future. 

#### How do I Exit?
I forgot that people sometimes want to close their applications via the command line.  When the program starts asking you to verify outputs, you can type "exit" to quit.  I'm a bit tired and will implement a better way to do this in the future. 

#### You Code Funny...
Hopefully my code is readable and and reusable for you.  I put a lot of effort into maintaining best practices and keeping true to the rules of "Low Coupling and High Cohesion".    It's a learning process and I welcome critique.  

Resources
-----

I used a few resources while building this project.  I'm super thankful for those who have done a lot of work previously. 

[I am Trask - A Neural Network in 11 Lines of Python](http://iamtrask.github.io/2015/07/12/basic-python-network/) - This piqued my intrest in Neural Networks when it popped up on Reddit recently. 

[The Nature of Code - Chapter 10: Neural Networks](http://natureofcode.com/book/chapter-10-neural-networks/) - This was often able to answer some of my questions and made for a great read. 

[C# Backpropagation Tutorial](http://www.codingvision.net/miscellaneous/c-backpropagation-tutorial-xor) - This was the initial C# project I looked at.  I took and modified a few elements that I really liked such as the Sigmoid and Neuron classes. 

[A Step by Step Backpropagation Example](http://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/) - This was an excellent explanation of Back-Propagation and helped me tremendously with some of the math involved. 

[Coding Neural Network Back-Propagation Using C#](https://visualstudiomagazine.com/Articles/2015/04/01/Back-Propagation-Using-C.aspx?Page=1) - This was another great C# example.  Dr. James McCaffrey (the author) has a lot of great insights in this article and others that he has written on the subject. 
