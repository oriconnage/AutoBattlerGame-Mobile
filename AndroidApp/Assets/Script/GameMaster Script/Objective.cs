using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Objective
{
  string getDescription();
  bool isCompleted();
  void UpdateObj();
    void Increase();
    void Deduct();
}
