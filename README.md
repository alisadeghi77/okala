# Answers to Technical Questions

1. **How long did you spend on the coding assignment? What would you add to your solution if you had more time?**
   - I spent about 4 ~ 5 hours on the coding assignment.
   - If I had more time, I would focus on the following improvements:
     - I would write more unit tests for diffrents scenarios.
     - I’d implement a Circuit Breaker with Polly to handle API failures.
     - The Result Pattern would be applied thoroughly, e.g: separate diffrent error cases explicitly.
     - I’d enhance exception handling with specific types and meaningful messages for better user and developer experience.

2. **What was the most useful feature that was added to the latest version of your language of choice?**
   - The most useful C# features I use are:
     - Primary Constructors for reducing code and more readable code
     - Records for immutable data models
     - File-Scoped Namespaces for cleaner code
     - Raw String Literals for easier multi-line string handling
       
     *I used some of this feature in my assigment*

3. **How would you track down a performance issue in production? Have you ever had to do this?**
     - For performance issues in a microservices project, we can use tools like Prometheus, OpenTelemetry, and others. While I don’t have real-world experience with these tools, I am familiar with how they work and their role in monitoring and diagnosing performance problems.

4. **How would you track down a performance issue in production? Have you ever had to do this?**
     - Last summer, I attended a conference on Domain-Driven Design (DDD), where I learned about bounded contexts, aggregates, and domain events. Over the past year, I’ve focused on reading about software architecture and .NET, particularly microservices and clean architecture.

5. **What do you think about this technical assessment?**
    - I think this technical assessment is well-designed because it does not need many time to develop and allows candidates to showcase their experience and creativity while also evaluating their ability to read and understand new documentation, grasp business requirements, and write documentation, etc. .
  
6. **Please, describe yourself using JSON**
    ```json
    {
      "firsName": "ali",
      "lastName": "sadeghi",
      "role": "Software Engineer",
      "bithDate": "1998-06-14",
      "skills": [
        ".NET",
        "C#",
        "sql",
        "postgres"
      ],
      "experience": "5 years",
      "education": {
        "degree": "Bachelor of Software Engineer",
        "university": "Islamic Azad Univercity"
      },
      "contact": {
        "email": "alisadeghi431@gmail.com",
        "phone": "09363364928"
      },
      "workExperience": [
        {
          "company": "Digipay",
          "role": ".net developer"
        },
        {
          "company": "Dadekavan",
          "role": "backend developer"
        },
        {
          "company": "Rabani",
          "role": ".net developer"
        },
        {
          "company": "ISTG",
          "role": ".net developer"
        }
      ]
    }
