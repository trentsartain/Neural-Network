# Neural Networks

Introduction
------------
If this is your first foray into Neural Networks, welcome!  I hope you enjoy yourself as much as I have.

This project is an attempt at creating an application that allows for quick interactions with a basic neural network. 

This project is written in C# and uses C# 6.0 Syntax.  You will need an environment that is capable of compiling the C# 6.0 syntax in order to use this program. 

![Alt text](ScreenShot.PNG?raw=true "Optional Title")

What is a Neural Network?
-----
Great question!  

A Neural Network can be thought of as a series of nodes (or neurons) that are interconnected, much like they are in the brain.  The network can have any number (N) of inputs and any number (M) of outputs.  In between the inputs and outputs are a series of "hidden" neurons that make up the hidden layers of the network.  These hidden layers provide the meat of the network and allow for some of the neat functionalities we can get out of a Neural Network.

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
	* This is the meat of the Neural Network.  This is where the magic happens.  The Neurons in this layer are assigned weights for each of their inputs.  These weights start off fairly random, but as the network is "trained" (discussed below), the weights are adjusted in order to make the neuron's output, and therefore the Neural Network's output closer to the expected result.
3. The Outputs
	* These are the outputs from the system. From the XOR example above, the output from the system would be either 'true' or 'false'.  In the Neural Network, the Outputs are the last line of Neurons.  These Neurons are also assigned a weight for each of their inputs and are "fed" by the Neurons in the hidden layer.  

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

In this project, the Neural Network is trained via the following common Neural Network training methods:

1. Back-Propagation
	* After each set of inputs is run through the system and an output generated, that output is validated against the expected output.  
	* The percentage of error that results is then propagated backwards (hence the name) through the Hidden Layers of the Neural Network.  This adjusts the weights assigned to each of a neuron's inputs in the Hidden Layers.  
	* Ideally, each Back-Propagation will bring the Neural Network's output closer to the expected output of the provided inputs.
2. Biases
	* Biases allow us to modify our activation function (discussed below) in order to generate a better output for each neuron.  
	* [See Here](http://stackoverflow.com/questions/2480650/role-of-bias-in-neural-networks) for an excellent explanation as to what a bias does for a Neural Network.  
3. Momentum
	* Used to prevent the system from converging to a local minimum. 
	* [See Here](https://en.wikibooks.org/wiki/Artificial_Neural_Networks/Neural_Network_Basics#Momentum)
4. Learning Rate
	* This will change the overall learning speed of the system.  
	* [See Here](https://en.wikibooks.org/wiki/Artificial_Neural_Networks/Neural_Network_Basics#Learning_Rate)

#### What defines a Neuron's Output?

A Neuron's output is defined by an [Activation Function](https://en.wikipedia.org/wiki/Activation_function).

In our case, we are using a [Sigmoid Function](https://en.wikipedia.org/wiki/Sigmoid_function) to define each Neuron's output.  The Sigmoid function will convert any value to a value between 0 and 1.  In the Neural Network, the Sigmoid functions will be used to generate initial weights and to help update percent errors.  

How Do I Use this Program?
------
This program is fairly simple to use and is divided into 3 main menus and a couple sub menus.

1. Main Menu
	* New Network - Manually input a network's configuration.
	* Import Network - Import a network configuration. (See JSON formatting section below)
	* Exit - Exit the program
2. Dataset Menu
	*  Type Dataset - Manually input datasets to be used.
	*  Import Dataset - Import a Dataset configuration.
	*  Test Network - Test the current network by typing in inputs.
	*  Export Network - Export the current network. (See JSON formatting section below)
	*  Main Menu - Go back to the Main Menu.
	*  Exit - Exit the program
3. Network Menu
	* Train Network - Train the current network based on parameters you give.
	* Test Network - Test the current network by typing in inputs.
	* Export Network - Export the current network. (See JSON formatting section below)
	* Export Dataset - Export the current dataset.
	* Dataset Menu - Go back to the Dataset Menu.
	* Main Menu - Go back to the Main Menu.
	* Exit - Exit the program


Suggested Neural Net JSON Formatting Standard
-----
As of yet, I haven't seen a standard serialized format for a Neural Network.  In an effort to implement importing and exporting within this program, I wrote a standardized format for the Neural Network so that a network exported from this program could be imported into another one with ease.  I imagine it will change a bit over time, but for now, this seems to work well. It's simple and hopefully transferrable.

Here's an example network that has been exported and can be re-imported into this program.  There are examples of this ("NetworkExample.txt") and a serialized Dataset ("DatasetExample.txt") inside of the DataExamples directory.
```javascript
{
  "LearnRate": 0.4,
  "Momentum": 0.9,
  "InputLayer": [
    {
      "Id": "f1fea97c-48ba-4e1b-b7eb-fe9d1897758c",
      "Bias": 0.88011651853104889,
      "BiasDelta": 0.0,
      "Gradient": 0.0,
      "Value": 0.0
    },
    {
      "Id": "fcf15808-fb11-4a04-9022-638140164c64",
      "Bias": -0.12904600665394495,
      "BiasDelta": 0.0,
      "Gradient": 0.0,
      "Value": 0.0
    }
  ],
  "HiddenLayers": [
    [
      {
        "Id": "2cc0c76b-1c40-4c72-803f-97c0997bcc07",
        "Bias": 1.6459098726963641,
        "BiasDelta": 6.8129688749504508E-06,
        "Gradient": 1.7032422187376126E-05,
        "Value": 0.83833657457730759
      },
      {
        "Id": "aeacc65b-101d-4a94-b9cc-ab008a36dfee",
        "Bias": -1.3229172632409467,
        "BiasDelta": -9.2984227388283447E-06,
        "Gradient": -2.3246056847070861E-05,
        "Value": 0.21033426055222876
      },
      {
        "Id": "fda1a91b-cb45-4fb0-9d58-b3571ef88777",
        "Bias": -0.43095589580981947,
        "BiasDelta": 1.2014607576953927E-05,
        "Gradient": 3.0036518942384814E-05,
        "Value": 0.39389960849056194
      }
    ]
  ],
  "OutputLayer": [
    {
      "Id": "9c096f95-800c-435b-9ee9-5af125abc8b3",
      "Bias": 3.8785304867945971,
      "BiasDelta": -5.2888861198121289E-06,
      "Gradient": -1.3222215299530321E-05,
      "Value": 0.0036428777527120928
    }
  ],
  "Synapses": [
    {
      "Id": "3c0e0f3f-beea-4754-af3d-4d00dca4c177",
      "OutputNeuronId": "2cc0c76b-1c40-4c72-803f-97c0997bcc07",
      "InputNeuronId": "f1fea97c-48ba-4e1b-b7eb-fe9d1897758c",
      "Weight": -4.9046816226955494,
      "WeightDelta": 0.0
    },
    {
      "Id": "201ac151-36b6-403d-a543-d77c0076e99f",
      "OutputNeuronId": "aeacc65b-101d-4a94-b9cc-ab008a36dfee",
      "InputNeuronId": "f1fea97c-48ba-4e1b-b7eb-fe9d1897758c",
      "Weight": 5.85326693338921,
      "WeightDelta": 0.0
    },
    {
      "Id": "b211a3a2-6deb-4411-8d51-2b47e22c4a10",
      "OutputNeuronId": "fda1a91b-cb45-4fb0-9d58-b3571ef88777",
      "InputNeuronId": "f1fea97c-48ba-4e1b-b7eb-fe9d1897758c",
      "Weight": 7.1642632394746659,
      "WeightDelta": 0.0
    },
    {
      "Id": "12caa1a0-cadd-4996-bef8-0ac8d0b84768",
      "OutputNeuronId": "2cc0c76b-1c40-4c72-803f-97c0997bcc07",
      "InputNeuronId": "fcf15808-fb11-4a04-9022-638140164c64",
      "Weight": 7.4919359894962705,
      "WeightDelta": 0.0
    },
    {
      "Id": "6469fb62-f273-4940-9eb2-b62323a1af0d",
      "OutputNeuronId": "aeacc65b-101d-4a94-b9cc-ab008a36dfee",
      "InputNeuronId": "fcf15808-fb11-4a04-9022-638140164c64",
      "Weight": 6.1679853662708544,
      "WeightDelta": 0.0
    },
    {
      "Id": "ac471b71-9850-4e16-acbb-bb650aa57de3",
      "OutputNeuronId": "fda1a91b-cb45-4fb0-9d58-b3571ef88777",
      "InputNeuronId": "fcf15808-fb11-4a04-9022-638140164c64",
      "Weight": -2.6579877134228087,
      "WeightDelta": 0.0
    },
    {
      "Id": "d1610867-c424-4588-a2d9-fdfea8135bb3",
      "OutputNeuronId": "9c096f95-800c-435b-9ee9-5af125abc8b3",
      "InputNeuronId": "2cc0c76b-1c40-4c72-803f-97c0997bcc07",
      "Weight": -9.5047366120987249,
      "WeightDelta": -4.4338666730127679E-06
    },
    {
      "Id": "4ca55472-aaa0-421c-993d-7e6b40ceb2a6",
      "OutputNeuronId": "9c096f95-800c-435b-9ee9-5af125abc8b3",
      "InputNeuronId": "aeacc65b-101d-4a94-b9cc-ab008a36dfee",
      "Weight": 10.58506658726629,
      "WeightDelta": -1.1124339511556304E-06
    },
    {
      "Id": "ab233d80-e39a-4033-bba1-afbc96696cef",
      "OutputNeuronId": "9c096f95-800c-435b-9ee9-5af125abc8b3",
      "InputNeuronId": "fda1a91b-cb45-4fb0-9d58-b3571ef88777",
      "Weight": -9.5151417793873545,
      "WeightDelta": -2.0832901719451647E-06
    }
  ]
}
```

What's Next? 
---
I'm not a fan of the current menu system, so that will probably be next along with a bunch of error handling. 

Code Considerations
---
This project is licensed under the terms of the MIT license.

#### Reusability
The Network and its supporting classes are self-contained, meaning that the "UI" portion of the program only serves to gather the necessary information to instantiate the Network object and its supporting classes.  You could theoretically take the Network and supporting classes and bring it into your own application with little to no modification. The network only requires the number of inputs, number of hidden neurons, the number of outputs and (optionally) a specified learning rate and momentum. 

#### You Code Funny...
Hopefully my code is readable and and reusable for you.  I put a lot of effort into maintaining best practices.  It's a learning process and I welcome critique.  

Resources
-----

I used a few resources while building this project.  I'm super thankful for those who have done a lot of work previously. 

[I am Trask - A Neural Network in 11 Lines of Python](http://iamtrask.github.io/2015/07/12/basic-python-network/) - This piqued my intrest in Neural Networks when it popped up on Reddit recently. 

[The Nature of Code - Chapter 10: Neural Networks](http://natureofcode.com/book/chapter-10-neural-networks/) - This was often able to answer some of my questions and made for a great read. 

[C# Backpropagation Tutorial](http://www.codingvision.net/miscellaneous/c-backpropagation-tutorial-xor) - This was the initial C# project I looked at.  I took and modified a few elements that I really liked such as the Sigmoid and Neuron classes. 

[A Step by Step Backpropagation Example](http://mattmazur.com/2015/03/17/a-step-by-step-backpropagation-example/) - This was an excellent explanation of Back-Propagation and helped me tremendously with some of the math involved. 

[Coding Neural Network Back-Propagation Using C#](https://visualstudiomagazine.com/Articles/2015/04/01/Back-Propagation-Using-C.aspx?Page=1) - This was another great C# example.  Dr. James McCaffrey (the author) has a lot of great insights in this article and others that he has written on the subject. 

[Simple C# Artificial Neural Network](http://www.craigsprogramming.com/2014/01/simple-c-artificial-neural-network.html) - This article played a large role in the November 2015 refactoring.  This convinced me to get rid of the layer class altogether and helped clean up the network training code.  
