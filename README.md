# Project 2 Report

Read the [project 2
specification](https://github.com/feit-comp30019/project-2-specification) for
details on what needs to be covered here. You may modify this template as you see fit, but please
keep the same general structure and headings.

Remember that you must also continue to maintain the Game Design Document (GDD)
in the `GDD.md` file (as discussed in the specification). We've provided a
placeholder for it [here](GDD.md).

## Table of Contents

- [Evaluation Plan](#evaluation-plan)
- [Evaluation Report](#evaluation-report)
- [Shaders and Special Effects](#shaders-and-special-effects)
- [Summary of Contributions](#summary-of-contributions)
- [References and External Resources](#references-and-external-resources)

# Evaluation Plan

## Evaluation Techniques
### 1. Observation Hybrid: Cooperative Evaluation - Post-Task Walkthrough

We have decided to utilize a hybrid of two observational techniques taught in class to leverage each technique's benefits. To avoid overlooking insights due to overwhelming tasks, observations will be limited to "levels" and short experiences at a time. 

For each individual, we will ask them to comment on any aspects of the game that they like or don't like during the walkthrough, rather than asking them to walk us through how they play the game. This attempts to reduce the "think aloud" awkwardness that can occur when one has to dictate their actions, simultaneously trying to avoid the awkward silences that can occur with the "cooperative evaluation" technique. Additionally, by opening the conversation up during gameplay, we can record insights and details that occur in real time without risking them being forgotten, which can occur with the "post-task walkthrough" technique. 

We aim to reduce the overhead the user faces while playing the game by recording the general "what", "why", "what next?" thought process from our own observations rather than dictation directly from the user, and limiting our questioning of those observations to points of confusion or anomalies. By asking the user to comment on emotions they feel and their likes and dislikes during the gameplay, we can record those comments and then utilize an informed "post-walkthrough" approach. Prior to the evaluation, we will have a prepared set of questions that can be adapted to user's comments to gain greater insights. A few adaptable questions for example:

* You mentioned [ aspect of game ] was [ visual description ]. What specifically made it [ unappealing or appealing]?
* What emotions did you feel during the gameplay? 
* Why was [ experience in game ] frustrating?
* What influenced that decision for you?
* At any point did your mind wander? [when, (if so) were you bored?]

Though there is the risk of experimenter bias influencing observations, we believe the trade-off with the insights we can obtain from our observation strategy has greater benefit.

### 2. Query Techniques
#### 2.1 Interviews
As mentioned above, we plan to conduct post-walkthrough interviews. By interviewing the users with adaptable questions, we hope to create an interview that is semi-structured. The structured aspect of the interview will help us ensure we ask crucial questions, and the adaptable aspect of the interview allows us to deviate for insight. 

Here is an example semi-structured interview.

#### 2.2 Scalar Questionnaire
To end the experiment, we plan to ask the user to fill out a quick anonymous usability questionnaire, based on post-study questionnaires for User Interaction Satisfaction (Brooke 1996). This aims to uncover any usability or frustrations that the user may have encountered but either felt too awkward to express (hence the anonymity), or too abstract to articulate in an interview format. Additionally, the scalar nature of the questionnaire will provide us an easy metric for analysis of our game, a good foundation for evaluating the user experience.  

Here is our planned usability questionnaire.

## Participants
Our target audience are late teens and early 20s that appreciate edgy themes, low poly aesthetics, and the bullet hell subgenre. 

### Recruiting Participants
Due to time constraints, most of our participants will be friends and fellow students that fit in the age demographic of our target audience. Our reasoning behind this is friends are likely to be willing to dedicate time and effort to our experiments, ensuring we have engaged participants that are comfortable sharing their thoughts. 

We recognize that this may introduce bias to our evaluation, however, the milestones timeline in this subject limits our options, so we believe this is the most effective recruitment strategy.

While we will aim to recruit friends that are experienced with games and have a good chance of appreciating our game's genre and aesthetics, we do not want to ignore the edge cases (still within our target audience) that could provide insight. These include inexperienced users, "uninterested" users, and users that do not have a personal relationship with any of our team. By "uninterested" users, we refer to users with either little interest in gaming or typically would not gravitate to this type of game genre. We plan on recruiting these users through classes, tutorials, and external activities, such as sports, workplaces, etc.

To summarize, we plan on recruiting the following:

| User Demographic  | Number of Users |
| ------------- |:-------------:|
| Experienced, interested | 8    |
| Inexperienced or "uninterested" | 3 |
| "Random"      | 3  |

Note: demographic categories may overlap. We will have at minimum 10 users.

### Qualifying Criteria
A benefit of recruiting friends is the knowledge of the user's interests prior to the interview, informing our selection of users primarily appreciative towards our game's genre and aesthetics. For those we are unfamiliar with, we plan to ask 3-5 basic screening questions that will inform us which category the user potentially fits in (random, inexperienced, uninterested, appreciative):

* Do you play video games? [ never, rarely, 1-2 times per week, etc. answer ]
* What are your favourite games? 
* What are your preferred game aesthetics?
* What are your favourite game genres?

**Important to note we will first seek consent from potential participants before asking questions and explain the anonymity of their identity and data, as well as clarify the purpose of the screening questions.**

## Data Collection
Our Data collection processes and type of data collected depends on our evaluation techniques. The below table shows our data collection process for each technique:

| Evaluation Technique  | Data Collected | Data Collection Process |
| ------------- |:-------------:|:-------------:|
| Screening Questionnaire | User age and occupation, game habits, game interests | In-person interview or online form* |
| Post-Walkthrough Interview | Emotions experienced, opinions on aesthetics, mechanics, and story, gameplay experience  | In-person/Zoom interview, face to face** |
| Scalar Usability Questionnaire | User scalar answers to questionnaire  | Online or paper form |
| Observations during Gameplay   | User body language, initial comments, opinions on game, decisions, confusion, emotions  | In-person or Zoom observation (shared screen)** |

*Dependent on consent given, either recorded audio, written notes, or both.

**Dependent on consent given, either recorded audio, recorded video, written notes, or a combination of the three.

We will provide a consent form to each user, outlining the different recording options and how the data from our experiments will be collected and used. Each user can withdraw at any time.

### Tools Used for Data Collection
While in-person gameplay observations and post-walkthrough interviews are preferred, we will utilize Zoom to conduct online interviews, as the share screen and recording features allow us to faciliate interviews in the case where in-person interviews are not an option.

We will use [Snapforms](https://snapforms.com.au/survey-monkey-alternative/) software for our questionnaires, as they are they Australian alternative to SurveyMonkey and they host their data on-shore.

Previous questionnaires have informed our current forms, including the basic usability questions from [SUS](https://doi.org/10.1201/9781498710411-35) and [Post-Study System Usability Questionnaire](https://trymata.com/learn/pssuq/#:~:text=The%20current%20iteration%20of%20the,end%20of%20a%20usability%20test.) (PSSUQ), and the desirability questions from the [Adoption Likelihood Factors Questionnaire](https://trymata.com/learn/alfq/) (ALFQ).

## Data Analysis
For the questionnaire, there will be two sections: usability and desirability. The questionnaire will mainly be comprised of usability questions, and using a scale for answers (rated on a scale 1-7), we will take the average score across users. Standard deviation calculations could also be informative, determining the variability in responses that have a wide range on the scale. The desirability section of the questionnaire aims to investigate user engagement.

For each observation and post-walkthrough interview, we will condense our qualitative findings into insights, and discuss the insights. Identified patterns, themes or common feedback can then be condensed into categories, and these categories will be discussed as a team to determine which are actionable.  

Comparing interview insights to the questionnaire results allows us to compare two forms of data, creating opportunity for our team to pinpoint discrepancies in our data collection processes. For example, positive qualitative feedback but negative usability scale scores would indicate that the game conceptually and aesthetically is engaging, but the gameplay may be frustrating.

After the evaluations are completed, insights are condensed and questionnaire scores are calculated, we will have a discussion as a team to decide which changes will be made. Priority will be given to the insights with the highest discrepancies, and the results with the most agreeability from users (e.g. all users rated [ feature a ] low on the usability scale).

## Timeline

| Week 10  | Recruitment and Preparation Stage  |
| ------------- |:-------------:|
| Sept 30 - Oct 1   | Finalizing evaluation plan   |
| Sept 29 - Oct 2 | Prepare materials (questionnaire and adaptable questions, surveys, consent forms) |
| By Oct 8 | Confirm participant availability, schedule evaluation meetings  |
| Prior to evaluation meetings | Send consent forms to participants |

| Week 11  | Observation and Interview Stage  |
| ------------- |:-------------:|
| Oct 7 - Oct 13   | Conduct evaluations, updating results after each evaluation takes place   |
| Oct 7 - Oct 13 | Condense insights and evaluation results, contributing to main insight document regularly |
| Oct 13 - 14 | Discuss evaluation results from interviews |

| Week 12 | Updating Stage|
| ------------- |:-------------:|
| Oct 14 | Submit gameplay video that incorporates early evaluation insights  |
| By Oct 15 | Discuss and identify changes to game, assign tasks |
| Oct 15 - 18 | Review all gathered data, add evaluation results to final report, and update GDD accordingly |
| Oct 15 - 26 | Add changes, "before and after" visuals to final report along with explanations

| SWOTVAC | Finalization Stage|
| ------------- |:-------------:|
| Oct 22 - 26 | Finalize game updates, continue to build final report  |
| Oct 26 - 29 | Testing of the game, regularly testing how the game builds, reviewing pr |
| Oct 29 - 31 | Submit final report and game |


# Evaluation Report

TODO (due milestone 3) - see specification for details

# Shaders and Special Effects

TODO (due milestone 3) - see specification for details

# Summary of Contributions

TODO (due milestone 3) - see specification for details

# References and External Resources
## References
Brooke, J. (1996). SUS: A “quick and dirty” usability scale. Usability Evaluation In Industry, 207–212. [doi 10.1201 9781498710411 35](https://doi.org/10.1201/9781498710411-35).

## External Resources
[Snapforms](https://snapforms.com.au/survey-monkey-alternative/)
