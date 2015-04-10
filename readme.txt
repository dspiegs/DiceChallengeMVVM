--Assumptions
  
Dice all have values as an sequential set of integers
  
--Explanations  
  
Rule Models use Func<> objects for customization

I could also "StraightRule" and "SequentialRule" that both implement IRule. 
The trade off is that if I did it this way extended rules would need to be added at design time, function allow for runtime rule creation. 
They could be stored in the db and rebuilt using the PredicateBuilder

