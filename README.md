# TURD

## Introduction
**TURD (Turbo Universal RISC Design)** is a simple architecture designed to accurately execute 3 specific programs. Optimized for a 9-bit instruction size, it minimizes instruction types and employs a stack-based register file to save bits by only using push and pop operations. Lacking dedicated registers like load-store architectures, TURD's instructions consist of immediates, operands, and operations. Immediates are pushed onto the stack, operands either push or pop from the stack, and operations use the top two stack elements, then push the result back.
Key features include:
Stack-based register file: Simplifies operand and operation management within the 9-bit constraint.
Streamlined instruction set: Categorized as immediates, operands, and operations for easier management.
Target program optimization: Tailored to efficiently run 3 specific programs.
RISC philosophy: Focuses on a smaller set of simple instructions for rapid execution and improved performance.
TURD's architecture offers an efficient solution, adhering to the 9-bit instruction size limit while optimizing performance for target programs. Its stack-based register file and simplified instruction set ensure both simplicity and accuracy.

## Individual Component Specification
### Top Level
File Name: top_level

Description: The top level module serves to connect all the individual modules of our processor. In this module, we instantiate all the necessary components listed below. The inputs to the module are clock, reset and the output is the done flag. This done flag is needed for the testbench to know when our processor has completed the individual programs. Additionally, the decoding of our machine code is also done in this module. Overall, the top level serves to connect all individual components and wires to create our processor.

